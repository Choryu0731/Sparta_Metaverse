using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlappyPlaneBaseUI : MonoBehaviour
{
    protected FlappyPlaneUIManager uiManager;

    public virtual void Init(FlappyPlaneUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
