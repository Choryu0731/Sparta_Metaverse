using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    private void Start()
    {
        int score = ScoreManager.Instance.GetScore();
        scoreText.text = $"Á¡¼ö : {score}";
    }
}
