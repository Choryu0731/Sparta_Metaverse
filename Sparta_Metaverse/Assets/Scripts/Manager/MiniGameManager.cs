using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartMiniGame(string sceneName)
    {

    }

    public void ExitMiniGame()
    {

    }
}
