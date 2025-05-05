using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlappyPlaneHomeUI : FlappyPlaneBaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(FlappyPlaneUIManager uiManager)
    {
        base.Init(uiManager);

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickStartButton()
    {
        uiManager?.OnClickStart();
    }

    private void OnClickExitButton()
    {
        uiManager?.OnClickExit();
    }
}
