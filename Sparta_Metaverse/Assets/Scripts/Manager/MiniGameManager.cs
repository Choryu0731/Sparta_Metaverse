using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    private string currentMiniGameScene = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void StartMiniGame(string sceneName)
    {
        currentMiniGameScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitMiniGame(int score)
    {
        ScoreManager.Instance.SaveHighScore(currentMiniGameScene, score);

        currentMiniGameScene = "";

        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }
}
