using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int flappyPlaneHighScore = 0;
    private int miniGame2HighScore = 0;

    private const string FlappyPlaneKey = "FlappyPlaneHighScore";
    private const string MiniGame2Key = "MiniGame2HighScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScores();
        }
        else Destroy(gameObject);
    }

    private void LoadHighScores()
    {
        flappyPlaneHighScore = PlayerPrefs.GetInt(FlappyPlaneKey, 0);
        miniGame2HighScore = PlayerPrefs.GetInt(MiniGame2Key, 0);
    }

    public void SaveHighScore(string gameName, int score)
    {
        if (gameName == "MiniGame_FlappyPlane")
        {
            if (score > flappyPlaneHighScore)
            {
                flappyPlaneHighScore = score;
                PlayerPrefs.SetInt(FlappyPlaneKey, flappyPlaneHighScore);
                PlayerPrefs.Save();
            }
        }
        else if (gameName == "MiniGame2Scene")
        {
            if(score > miniGame2HighScore)
            {
                miniGame2HighScore = score;
                PlayerPrefs.SetInt(MiniGame2Key, miniGame2HighScore);
                PlayerPrefs.Save();
            }
        }
    }

    public int GetHighScore(string gameName)
    {
        if(gameName == "MiniGame_FlappyPlane")
        {
            return flappyPlaneHighScore;
        }
        else if (gameName == "MiniGame2Scene")
        {
            return miniGame2HighScore;
        }

        return 0;
    }
}
