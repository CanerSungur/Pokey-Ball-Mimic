using UnityEngine;

public class MoveBall : MonoBehaviour
{
    /*
     * 
     * Stricts balls movement only on Stick's Y axis.
     * Calculates distance between ball and the stick. Decides to throw or return to its position.
     * If ball is thrown, it changes state to ThrowBall.
     * 
     */

    [Header("Unity Setup Field")]
    private Touch touch;

    [Header("Ball Positioning Properties")]
    private float distance;
    private Vector3 centerPos;
    private GameObject stick;

    [Header("Ball Movement Properties")]
    private float speed;

    private void Start()
    {
        stick = GameObject.Find("Stick");
        speed = 0.001f;
        centerPos = stick.transform.position;
    }

    private void Update()
    {
        if (Ball.BallCurrentState == Ball.State.Still || Ball.BallCurrentState == Ball.State.Dragged)
        {
            //Distance between ball and the stick.
            centerPos = new Vector3(0f, stick.transform.position.y - 0.007f, -1f);
            distance = Vector3.Distance(transform.position, centerPos);

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Ball.BallCurrentState = Ball.State.Dragged;

                    //Limit ball's axis values according to stick's transform.
                    transform.position = new Vector3(
                            transform.position.x,
                            Mathf.Clamp(transform.position.y, stick.transform.position.y - 0.21f, stick.transform.position.y - 0.007f) + touch.deltaPosition.y * speed,
                            transform.position.z);

                    #region Decide Ball Thrust Power

                    if (distance > 0.05f && distance < 0.08f)
                    {
                        Ball.BallThrowPower = Ball.ThrowPower.Slow;
                    }
                    else if (distance >= 0.08f && distance < 0.14f)
                    {
                        Ball.BallThrowPower = Ball.ThrowPower.Medium;
                    }
                    else if (distance >= 0.14f)
                    {
                        Ball.BallThrowPower = Ball.ThrowPower.Fast;
                    }

                    #endregion

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (distance <= 0.05f)
                    {
                        Ball.BallCurrentState = Ball.State.Still;

                        //If it's not pulled enough, return ball to its origin point.
                        transform.position = centerPos;
                    }
                    else
                    {
                        Ball.BallCurrentState = Ball.State.Thrown;

                        //Add Force to the ball and disable all restrictions.
                    }
                }
            }
        }
    }
}
