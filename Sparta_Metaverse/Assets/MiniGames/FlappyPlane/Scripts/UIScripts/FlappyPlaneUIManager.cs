using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState
{
    Home,
    Game,
    Score
}
public class FlappyPlaneUIManager : MonoBehaviour
{
    static FlappyPlaneUIManager instance;
    public static FlappyPlaneUIManager Instance
    {
        get
        {
            return instance;
        }
    }

    UIState currentState = UIState.Home;

    FlappyPlaneHomeUI homeUI = null;

    FlappyPlaneGameUI gameUI = null;

    FlappyPlaneScoreUI scoreUI = null;

    FlappyPlaneGameManager gameManager = null;

    private void Awake()
    {
        instance = this;
        gameManager = FindObjectOfType<FlappyPlaneGameManager>();

        homeUI = GetComponentInChildren<FlappyPlaneHomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<FlappyPlaneGameUI>(true);
        gameUI?.Init(this);
        scoreUI = GetComponentInChildren<FlappyPlaneScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(UIState.Home);
    }

    public void ChangeState (UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }
    
    public void OnClickStart()
    {
        ChangeState(UIState.Game);
        gameManager?.StartGame();
    }

    public void OnClickExit()
    {
        int score = gameManager?.CurrentScore ?? 0;
        MiniGameManager.Instance.EndMiniGame(score); // ���ھ� ȭ�鿡�� ���� �� ���� ����
    }

    public void OnClickRestart()
    {
        ChangeState(UIState.Home);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetScoreUI()
    {
        ChangeState(UIState.Score);
        int currentScore = gameManager?.CurrentScore ?? 0;

        ScoreManager.Instance.SaveHighScore("MiniGame_FlappyPlane", currentScore);

        int highScore = ScoreManager.Instance.GetHighScore("MiniGame_FlappyPlane");
        scoreUI?.UpdateScore(gameManager?.CurrentScore ?? 0, highScore); // �̹��� ������ �ְ����� ����
    }

    public void UpdateScore(int score)
    {
        gameUI?.UpdateScore(score);
    }
}
