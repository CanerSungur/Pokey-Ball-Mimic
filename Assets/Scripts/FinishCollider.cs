using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinishCollider : MonoBehaviour
{
    /*
     * 
     * Changes next level UI color to yellow.
     * Destroys ball when it collides second time and change game state to LevelWon.
     * After triggering first time, lets player to tap just one time.
     * Enables and disables 'Last Poke' screen.
     * 
     */

    [Header("Unity Setup Field")]
    public Image nextLevelBackground;
    public Color colorFinished;
    [HideInInspector] public bool didBallCollided;
    public GameObject lastPokeScreen;

    private void Start()
    {
        didBallCollided = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            //Collision for the first time.
            if (!didBallCollided)
            {
                nextLevelBackground.color = colorFinished;
                didBallCollided = true;

                StartCoroutine(ScreenPopUp(lastPokeScreen));
            }
            //Collision for the second time
            else
            {
                //Destroy Ball and win level
                lastPokeScreen.SetActive(false);
                GameManager.LevelWon = true;
            }
        }
    }

    private IEnumerator ScreenPopUp(GameObject screenObject)
    {
        screenObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        screenObject.SetActive(false);
    }
}
