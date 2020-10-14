using UnityEngine;

public class Coin : MonoBehaviour
{
    /*
     * 
     * Destroys coin and increases player gold.
     * Enables CoinUI then coin is collected.
     * 
     */

    [Header("Unity Setup Field")]
    public GameObject coinUI;

    private void Start()
    {
        coinUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            GameManager.Coin++;
            coinUI.SetActive(true);
            
            Destroy(gameObject);
        }
    }  
}
