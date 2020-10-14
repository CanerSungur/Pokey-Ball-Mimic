using UnityEngine;
using UnityEngine.UI;

public class ProgressionBar : MonoBehaviour
{
    /*
     * 
     * Fills the progress bar.
     * 
     */

    [Header("Unity Setup Field")]
    public Image mask;
    private Ball ball;
    private FinishCollider finishCollider;

    private void Start()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        finishCollider = GameObject.Find("Finish Collider").GetComponent<FinishCollider>();
    }

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        if (!finishCollider.didBallCollided)
        { 
            //Calculate fill amount.
            float fillAmount = ball.maxReach / ball.maxDistance;
            mask.fillAmount = fillAmount;
        }
        else
        {
            //if we pass finish line, fill the bar automaticly.
            mask.fillAmount = 1f;
        }
    }
}
