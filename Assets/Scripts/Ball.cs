using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /*
     * 
     * Holds ball's state and throw power info.
     * Activates and deactivates scripts accordingly.
     * Enables and disables stick mesh according to the state.
     * 
     */

    [Header("Ball Static Field")]
    public static State BallCurrentState;
    public static ThrowPower BallThrowPower;

    [Header("Ball Scripts")]
    private MoveBall moveBallScript;
    private ThrowBall throwBallScript;

    [Header("Unity Setup Field")]
    private Touch touch;
    public GameObject stickMesh;
    private Rigidbody rb;
    private Stick stickCollisionScript;

    //We'll calculate progression according to ball's y axis.
    //Progression starts from 0.49f, ends at 50f. Meaning, distance is 49.51f.
    [Header("Ball Level Progression")]
    [HideInInspector] public float maxReach;
    [HideInInspector] public float maxDistance;

    [Header("After Finish Line")]
    private FinishCollider finishCollider;

    public enum State
    {
        Still,
        Dragged,
        Thrown,
        Freeze,
        Falling
    };

    public enum ThrowPower
    {
        NoPower,
        Slow,
        Medium,
        Fast
    };

    private void Start()
    {
        BallCurrentState = State.Still;
        BallThrowPower = ThrowPower.NoPower;

        moveBallScript = gameObject.GetComponent<MoveBall>();
        throwBallScript = gameObject.GetComponent<ThrowBall>();

        rb = gameObject.GetComponent<Rigidbody>();
        stickCollisionScript = GameObject.Find("Stick").GetComponent<Stick>();
        
        maxDistance = 49.51f;

        finishCollider = GameObject.Find("Finish Collider").GetComponent<FinishCollider>();
    }

    private void Update()
    {
        #region Ball's Progression

        if (BallCurrentState == State.Still)
        {
            if (maxReach < transform.position.y - 0.49f)
            {
                if (maxReach < maxDistance)
                    maxReach = transform.position.y - 0.49f;
                else
                    maxReach = maxDistance;

                GameManager.Score = (int)(maxReach * 9);
            }
        }

        #endregion

        #region Ball State Descriptions

        if (BallCurrentState == State.Still)
        {
            BallThrowPower = ThrowPower.NoPower;

            moveBallScript.enabled = true;
            throwBallScript.enabled = false;
            stickMesh.SetActive(true);
        }

        else if (BallCurrentState == State.Thrown)
        {
            moveBallScript.enabled = false;
            throwBallScript.enabled = true;
            stickMesh.SetActive(false);

            #region Ball's Touch Control In Air

            //if player taps while the ball is on platform, change ball state to still.
            if (stickCollisionScript.isOnPlatform)
            {
                if (!finishCollider.didBallCollided)
                {
                    if (Input.touchCount > 0)
                    {
                        touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            BallCurrentState = State.Still;
                        }
                    }
                }
                //if the ball passed the finish line
                else
                {
                    //We let player touch one time and then ball gets freeze and fall state.
                    if (Input.touchCount > 0)
                    {
                        touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began)
                        {
                            StartCoroutine(FreezeAndFall());
                        }
                    }
                }
            }

            #endregion
        }

        else if (BallCurrentState == State.Freeze)
        {
            BallThrowPower = ThrowPower.NoPower;

            moveBallScript.enabled = false;
            throwBallScript.enabled = false;
            stickMesh.SetActive(true);
        }

        else if (BallCurrentState == State.Falling)
        {
            BallThrowPower = ThrowPower.NoPower;

            moveBallScript.enabled = false;
            throwBallScript.enabled = false;
            stickMesh.SetActive(false);
        }

        #endregion
    }

    private void FixedUpdate()
    {
        if (BallCurrentState == State.Still || BallCurrentState == State.Freeze)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else if (BallCurrentState == State.Dragged)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
        }
        else if (BallCurrentState == State.Thrown  || BallCurrentState == State.Falling)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        else if (BallCurrentState == State.Falling)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager.GameIsOver = true;
        }
    }

    private IEnumerator FreezeAndFall()
    {
        //Freeze the ball.
        BallCurrentState = State.Freeze;
        
        yield return new WaitForSeconds(2f);
        
        //Start falling
        BallCurrentState = State.Falling;
    }
}
