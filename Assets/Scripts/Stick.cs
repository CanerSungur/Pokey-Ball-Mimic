using UnityEngine;

public class Stick : MonoBehaviour
{
    /*
     * 
     * If stick touches something other than Platform or Bonus Box, poking does not work.
     * It ignores collide with the ball, because stick always collides with the ball.
     * 
     */

    [Header("Unity Setup Fields")]
    public GameObject stickMesh;
    [HideInInspector] public bool isOnPlatform;

    private void Start()
    {
        isOnPlatform = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "BonusBox")
            {
                isOnPlatform = true;
            }
            else
                isOnPlatform = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            isOnPlatform = false;
        }
    }
}
