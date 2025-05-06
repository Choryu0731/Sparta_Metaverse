using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // ���� UI ���� ������...
    public GameObject vehicleUI;
    public GameObject customizeUI;
    public GameObject leaderboardUI;
    public GameObject mapGuideUI;
    public TextMeshProUGUI mapGuideText;

    // ���� �߰��� �̴ϰ��� Ȯ�� UI ���� ����
    public GameObject miniGameConfirmUI;
    public Button miniGameConfirmYesButton;
    public Button miniGameConfirmNoButton;
    public TextMeshProUGUI miniGameConfirmText;

    private string miniGameSceneName; // ������ �̴ϰ��� �� �̸�
    private Action onMiniGameCancel;   // ��� �� ������ �׼� (���� ����)

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

    public void OpenMiniGameConfirmUI(string sceneName, string message = "�̴ϰ����� �����Ͻðڽ��ϱ�?", Action onCancel = null)
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

    // Yes ��ư Ŭ�� �� �̴ϰ��� ����
    private void StartMiniGame()
    {
        Debug.Log("StartMiniGame() ȣ���, sceneName: " + miniGameSceneName);
        if (!string.IsNullOrEmpty(miniGameSceneName))
        {
            MiniGameManager.Instance.StartMiniGame(miniGameSceneName);
            CloseMiniGameConfirmUI();
        }
        else
        {
            Debug.LogWarning("miniGameSceneName�� ����ֽ��ϴ�.");
        }
    }

    // No ��ư Ŭ�� �� �̴ϰ��� Ȯ�� UI �ݱ� �� ��� �׼� ���� (���� ����)
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
