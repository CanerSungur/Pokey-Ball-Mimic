using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
     * 
     * Makes camera follow the ball.
     * If ball is in Fast Throw stage, camera zooms out and then zooms back in.
     * 
     */

    [Header("Unity Setup Field")]
    private GameObject ball;
    private Vector3 cameraOffset;
    private Camera cam;
    private float camPanSpeed;

    private void Start()
    {
        ball = GameObject.Find("Ball");
        cam = gameObject.GetComponent<Camera>();

        camPanSpeed = 15f;
        cameraOffset = new Vector3(1.1f, 0.67f, -2.6f);
    }

    private void Update()
    {
        if (!GameManager.GameIsOver && !GameManager.LevelWon)
        {
            transform.position = ball.transform.position + cameraOffset;
        }

        if (Ball.BallCurrentState == Ball.State.Dragged && Ball.BallThrowPower == Ball.ThrowPower.Fast)
        {
            CameraZoomOut(75f);
        }
        else
        {
            CameraZoomIn(60f);
        }
    }

    private void CameraZoomIn(float fov)
    {
        if (cam.fieldOfView >= fov)
        {
            cam.fieldOfView -= Time.deltaTime * camPanSpeed;
        }
    }

    private void CameraZoomOut(float fov)
    {
        if (cam.fieldOfView <= fov)
        {
            cam.fieldOfView += Time.deltaTime * camPanSpeed;
        }
    }
}
