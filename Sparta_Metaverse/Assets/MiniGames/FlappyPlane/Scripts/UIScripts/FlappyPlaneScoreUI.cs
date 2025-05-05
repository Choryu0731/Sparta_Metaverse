using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlappyPlaneScoreUI : FlappyPlaneBaseUI
{
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(FlappyPlaneUIManager uiManager)
    {
        base.Init(uiManager);

        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void UpdateScore(int currentScore, int highScore)
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = currentScore.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }

    private void OnClickRestartButton()
    {
        uiManager?.OnClickRestart();
    }

    private void OnClickExitButton()
    {
        uiManager?.OnClickExit();
    }
}
