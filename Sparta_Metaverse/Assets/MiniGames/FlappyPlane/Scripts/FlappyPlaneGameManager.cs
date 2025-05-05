using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyPlaneGameManager : MonoBehaviour
{
    static FlappyPlaneGameManager FlappyPlanegameManager;
    public static FlappyPlaneGameManager Instance { get { return FlappyPlanegameManager; } }

    private int currentScore = 0;
    public int CurrentScore {  get { return currentScore; } }

    public int highScore = 0;
    public int HighScore { get { return highScore; } }

    FlappyPlaneUIManager FlappyPlaneuiManager;
    public FlappyPlaneUIManager FlappyPlaneUIManager { get { return FlappyPlaneuiManager; } }

    private const string HighScoreKey = "FlappyPlaneHighScore";

    [SerializeField] private FlappyPlanePlayer player;
    [SerializeField] private float initialTimeScale = 0f;
    [SerializeField] private float gameTimeScale = 1f;

    private bool isGameStarted = false;
    public bool IsGameStarted { get { return isGameStarted; } }


    private void Awake()
    {
        FlappyPlanegameManager = this;
        FlappyPlaneuiManager = FindObjectOfType<FlappyPlaneUIManager>();

        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        Time.timeScale = initialTimeScale;
    }

    private void Start()
    {
        FlappyPlaneuiManager.UpdateScore(currentScore);

        if (player != null)
        {
            player.enabled = false;
            player.isDead = true;
        }
    }

    public void StartGame()
    {
        if (!isGameStarted)
        { 
            isGameStarted = true;
            currentScore = 0;
            FlappyPlaneuiManager.UpdateScore(currentScore);

            Time.timeScale = gameTimeScale;

            if (player != null)
            {
                player.enabled = true;
                player.isDead = false;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameStarted = false;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        FlappyPlaneuiManager.SetScoreUI();
    }

    public void RestartGame()
    {
        isGameStarted = false;
        Time.timeScale = gameTimeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        if (isGameStarted)
        {
            currentScore += score;
            Debug.Log("Score : " + currentScore);
            FlappyPlaneuiManager.UpdateScore(currentScore);
        }
    }
}
