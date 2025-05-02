using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
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

    public Text scoreText;
    private void Start()
    {
        int score = ScoreManager.Instance.GetScore();
        scoreText.text = $"Á¡¼ö : {score}";
    }
}
