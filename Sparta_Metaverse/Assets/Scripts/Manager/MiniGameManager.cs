using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    private string currentMiniGameScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartMiniGame(string sceneName)
    {
        Debug.Log($"MiniGameManager: Starting mini-game scene: {sceneName}");
        currentMiniGameScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
    public void EndMiniGame(int score)
    {
        UIManager.Instance?.UpdateLeaderboardUI();

        SceneManager.LoadScene("MainScene");
        currentMiniGameScene = null;
    }

    public string GetCurrentMiniGameScene()
    {
        return currentMiniGameScene;
    }
}
