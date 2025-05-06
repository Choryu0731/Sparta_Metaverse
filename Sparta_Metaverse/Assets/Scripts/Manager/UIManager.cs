using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // 기존 UI 관련 변수들...
    public GameObject vehicleUI;
    public GameObject customizeUI;
    public GameObject leaderboardUI;
    public GameObject mapGuideUI;
    public TextMeshProUGUI mapGuideText;

    // 새로 추가할 미니게임 확인 UI 관련 변수
    public GameObject miniGameConfirmUI;
    public Button miniGameConfirmYesButton;
    public Button miniGameConfirmNoButton;
    public TextMeshProUGUI miniGameConfirmText;

    private string miniGameSceneName; // 시작할 미니게임 씬 이름
    private Action onMiniGameCancel;   // 취소 시 실행할 액션 (선택 사항)

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
    }

    public void OpenVehicleUI() { if (vehicleUI != null) vehicleUI.SetActive(true); }
    public void CloseVehicleUI() { if (vehicleUI != null) vehicleUI.SetActive(false); }
    public void OpenCustomizeUI() { if (customizeUI != null) customizeUI.SetActive(true); }
    public void CloseCustomizeUI() { if (customizeUI != null) customizeUI.SetActive(false); }
    public void OpenLeaderboard() { if (leaderboardUI != null) leaderboardUI.SetActive(true); }
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

    // Yes 버튼 클릭 시 미니게임 시작
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

    // No 버튼 클릭 시 미니게임 확인 UI 닫기 및 취소 액션 실행 (선택 사항)
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
}
