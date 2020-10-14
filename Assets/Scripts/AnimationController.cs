using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /*
     * 
     * Controls the animation in object's children first, after that in itself.
     * 
     */

    [Header("Unity Setup Field")]
    private Animation anim;

    private void Start()
    {
        //Try to get Animation from children. If it does not exist, then get its own Animation component.
        anim = gameObject.GetComponentInChildren<Animation>();
        if (anim == null)
        {
            anim = gameObject.GetComponent<Animation>();
        }
    }

    private void Update()
    {
        if (Ball.BallCurrentState == Ball.State.Dragged && Ball.BallThrowPower == Ball.ThrowPower.Fast)
        {
            anim.Play();
        }
        else
        {
            anim.Stop();
        }
    }
}
