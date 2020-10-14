using UnityEngine;

public class StickRotation : MonoBehaviour
{
    /*
     * 
     * Rotates the stick from stick rotation point, according to the ball.
     * 
     */

    [Header("Unity Setup Field")]
    private GameObject ball;
    private float followSpeed;

    private void Start()
    {
        ball = GameObject.Find("Ball");
        followSpeed = 50f;
    }

    private void FixedUpdate()
    {
        RotateToBall();
    }

    private void RotateToBall()
    {
        Vector3 direction = ball.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * followSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(-rotation.x, 0f, 0f);
    }
}
