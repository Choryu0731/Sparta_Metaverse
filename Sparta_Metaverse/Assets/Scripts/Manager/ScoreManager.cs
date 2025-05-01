using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int lastScore;

    private void Awake()
    {
        Instance = this;
    }

    public void SaveScore(int score)
    {
        lastScore = score;
    }

    public int GetScore()
    {
        return lastScore;
    }
}
