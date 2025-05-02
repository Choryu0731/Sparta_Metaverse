using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyPlaneGameManager : MonoBehaviour
{
    static FlappyPlaneGameManager FlappyPlanegameManager;
    public static FlappyPlaneGameManager Instance { get { return FlappyPlanegameManager; } }

    private int currentScore = 0;

    FlappyPlaneUIManager FlappyPlaneuiManager;
    public FlappyPlaneUIManager FlappyPlaneUIManager { get { return FlappyPlaneuiManager; } }

    private void Awake()
    {
        FlappyPlanegameManager = this;
        FlappyPlaneuiManager = FindObjectOfType<FlappyPlaneUIManager>();
    }

    private void Start()
    {
        FlappyPlaneuiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        FlappyPlaneuiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score : " + currentScore);
        FlappyPlaneuiManager.UpdateScore(currentScore);
    }
}
