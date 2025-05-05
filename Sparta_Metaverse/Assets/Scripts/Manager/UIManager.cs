using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
    }
    public void OpenVehicleUI()
    {

    }

    public void OpenCustomizeUI()
    {

    }

    public void OpenLeaderboard()
    {

    }

    public void OpenMapGuide(string message)
    {
        Debug.Log(message);
    }

    
}
