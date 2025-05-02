using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    private string miniGameScene = "MiniGame_FlappyBird";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StartMiniGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitMiniGame(int score)
    {
        ScoreManager.Instance.SaveScore(score);
        SceneManager.UnloadSceneAsync(miniGameScene);
    }
}
