using System.Collections;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    /*
     * 
     * Adds relevant coin to player, if player can tap in 'Last Poke' part.
     * If coin is increased, enables coinui.
     * 
     */

    [Header("Unity Setup Field")]
    public int coinContaining;
    private bool didCoinIncrease;
    public GameObject coinUI;

    private void Start()
    {
        didCoinIncrease = false;
        coinUI.SetActive(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Stick")
        {
            if (!didCoinIncrease && Ball.BallCurrentState == Ball.State.Freeze)
            {
                //Meaning, last poke happened on this very box.
                GameManager.Coin += coinContaining;
                coinUI.SetActive(true);

                didCoinIncrease = true;
            }
        }
    }
}
