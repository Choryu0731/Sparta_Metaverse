using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlappyPlaneGameUI : FlappyPlaneBaseUI
{
    [SerializeField] private TMP_Text scoreText;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(FlappyPlaneUIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
