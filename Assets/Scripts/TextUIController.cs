using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextUIController : MonoBehaviour
{
    /*
     * 
     * Updates and writes UI texts to the screen.
     * When coinUI is enabled, script waits for 2.5 seconds and disables it.
     * 
     */

    [Header("UI Text Objects")]
    public Text scoreText;
    public Text coinText;
    public GameObject coinUI;

    private void Update()
    {
        scoreText.text = GameManager.Score.ToString();
        coinText.text = GameManager.Coin.ToString();

        if (coinUI.activeSelf)
        {
            StartCoroutine(DisableCoinUIAfterSeconds());
        }
    }

    private IEnumerator DisableCoinUIAfterSeconds()
    {
        yield return new WaitForSeconds(2.5f);
        coinUI.SetActive(false);
    }
}
