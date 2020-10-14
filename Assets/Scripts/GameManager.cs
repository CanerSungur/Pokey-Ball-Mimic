using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
     * 
     * Manages game's state, player score and coin.
     * 
     */

    [Header("Game States")]
    public static bool GameIsOver;
    public static bool LevelWon;

    [Header("Unity Setup Fields")]
    private MoveBall moveBallScript;
    private ThrowBall throwBallScript;
    private GameObject ball;

    [Header("Screen Setup Field")]
    public GameObject gameOverScreen;
    public GameObject levelWonScreen;

    [Header("Player Info Field")]
    public static int Score;
    public static int Coin;

    private void Start()
    {
        Time.timeScale = 1f;

        GameIsOver = false;
        LevelWon = false;
        Score = 0;
        Coin = 0;

        moveBallScript = GameObject.Find("Ball").GetComponent<MoveBall>();
        moveBallScript.enabled = true;
        throwBallScript = GameObject.Find("Ball").GetComponent<ThrowBall>();
        throwBallScript.enabled = false;

        ball = GameObject.Find("Ball");

        gameOverScreen.SetActive(false);
        levelWonScreen.SetActive(false);
    }

    private void Update()
    {
        if (GameIsOver)
        {
            //Enable game over screen.
            Time.timeScale = 0f;

            moveBallScript.enabled = false;
            throwBallScript.enabled = false;

            gameOverScreen.SetActive(true);
        }

        if (!GameIsOver && LevelWon)
        {
            //Enable win level screen and destroy the ball.
            Time.timeScale = 0f;
            Destroy(ball);

            levelWonScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
