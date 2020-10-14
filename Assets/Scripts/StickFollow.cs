using UnityEngine;

public class StickFollow : MonoBehaviour
{
    /*
     * 
     * Makes stick follow 'stick rotate point' which is child of the ball.
     * 
     */

    [Header("Unity Setup Field")]
    private GameObject stickRotatePoint;

    private void Start()
    {
        stickRotatePoint = GameObject.Find("Stick Rotate Point");
    }

    private void Update()
    {
        if (Ball.BallCurrentState == Ball.State.Thrown && !GameManager.GameIsOver && !GameManager.LevelWon)
        {
            transform.position = new Vector3(stickRotatePoint.transform.position.x, stickRotatePoint.transform.position.y, stickRotatePoint.transform.position.z);
        }
    }
}
