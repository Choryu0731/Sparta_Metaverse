using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject vehicleUI;
    public GameObject customizeUI;
    public GameObject leaderboardUI;
    public GameObject mapGuideUI;
    public TextMeshProUGUI mapGuideText;

    public GameObject miniGameConfirmUI;
    public Button miniGameConfirmYesButton;
    public Button miniGameConfirmNoButton;
    public TextMeshProUGUI miniGameConfirmText;

    [Header("Leaderboard UI")]
    public TextMeshProUGUI flappyPlaneHighScoreText;
    public TextMeshProUGUI miniGame2HighScoreText;

    private string miniGameSceneName;
    private Action onMiniGameCancel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (vehicleUI != null) vehicleUI.SetActive(false);
        if (customizeUI != null) customizeUI.SetActive(false);
        if (leaderboardUI != null) leaderboardUI.SetActive(false);
        if (mapGuideUI != null) mapGuideUI.SetActive(false);

        if (miniGameConfirmUI != null) miniGameConfirmUI.SetActive(false);
        if (miniGameConfirmYesButton != null)
        {
            miniGameConfirmYesButton.onClick.AddListener(StartMiniGame);
        }
        if (miniGameConfirmNoButton != null)
        {
            miniGameConfirmNoButton.onClick.AddListener(CloseMiniGameConfirmUI);
        }

        if (leaderboardUI != null) leaderboardUI.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void OpenVehicleUI() { if (vehicleUI != null) vehicleUI.SetActive(true); }
    public void CloseVehicleUI() { if (vehicleUI != null) vehicleUI.SetActive(false); }
    public void OpenCustomizeUI() { if (customizeUI != null) customizeUI.SetActive(true); }
    public void CloseCustomizeUI() { if (customizeUI != null) customizeUI.SetActive(false); }
    public void OpenLeaderboard() 
    {
        if (leaderboardUI != null)
        {
            leaderboardUI.SetActive(true);
            UpdateLeaderboardUI();
        }
    }
    public void CloseLeaderboard() { if (leaderboardUI != null) leaderboardUI.SetActive(false); }
    public void OpenMapGuide(string text) { if (mapGuideUI != null) { mapGuideUI.SetActive(true); mapGuideText.text = text; } }
    public void CloseMapGuide() { if (mapGuideUI != null) mapGuideUI.SetActive(false); }

    public void OpenMiniGameConfirmUI(string sceneName, string message = "미니게임을 시작하시겠습니까?", Action onCancel = null)
    {
        if (miniGameConfirmUI != null)
        {
            miniGameConfirmUI.SetActive(true);
            if (miniGameConfirmText != null)
            {
                miniGameConfirmText.text = message;
            }
            miniGameSceneName = sceneName;
            onMiniGameCancel = onCancel;
        }
    }

    private void StartMiniGame()
    {
        Debug.Log("StartMiniGame() 호출됨, sceneName: " + miniGameSceneName);
        if (!string.IsNullOrEmpty(miniGameSceneName))
        {
            MiniGameManager.Instance.StartMiniGame(miniGameSceneName);
            CloseMiniGameConfirmUI();
        }
        else
        {
            Debug.LogWarning("miniGameSceneName이 비어있습니다.");
        }
    }

    private void CloseMiniGameConfirmUI()
    {
        if (miniGameConfirmUI != null)
        {
            miniGameConfirmUI.SetActive(false);
            miniGameSceneName = null;
            onMiniGameCancel?.Invoke();
            onMiniGameCancel = null;
        }
    }

    public void UpdateLeaderboardUI()
    {
        Debug.Log("리더보드 점수 불러오기");

        int flappyScore = PlayerPrefs.GetInt("FlappyPlaneHighScore", 0);
        int miniGame2Score = PlayerPrefs.GetInt("MiniGame2HighScore", 0);

        if (flappyPlaneHighScoreText != null)
            flappyPlaneHighScoreText.text = flappyScore.ToString();

        if (miniGame2HighScoreText != null)
            miniGame2HighScoreText.text = miniGame2Score.ToString();
    }
}
