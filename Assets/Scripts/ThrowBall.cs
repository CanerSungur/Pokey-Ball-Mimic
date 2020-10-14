using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    /*
     * 
     * Makes the ball thrown in the air.
     * 
     */

    private Rigidbody rb;

    private float throwForce;
    private bool didItThrown;

    private void OnEnable()
    {
        //We want variable to be false every time we enable the script.
        didItThrown = false;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        throwForce = 10f;
    }

    private void FixedUpdate()
    {
        //Add Force according to thrust power
        if (Ball.BallThrowPower == Ball.ThrowPower.Slow)
        {
            if (!didItThrown)
            {
                //rb.useGravity = true;
                rb.AddRelativeForce(Vector3.forward * throwForce * 0.3f, ForceMode.Impulse);

                didItThrown = true;
            }
        }
        else if (Ball.BallThrowPower == Ball.ThrowPower.Medium)
        {
            if (!didItThrown)
            {
                //rb.useGravity = true;
                rb.AddRelativeForce(Vector3.forward * throwForce * 0.6f, ForceMode.Impulse);

                didItThrown = true;
            }
        }
        else if (Ball.BallThrowPower == Ball.ThrowPower.Fast)
        {
            if (!didItThrown)
            {
                //rb.useGravity = true;
                rb.AddRelativeForce(Vector3.forward * throwForce * 1f, ForceMode.Impulse);

                didItThrown = true;
            }
        }
    }
}
