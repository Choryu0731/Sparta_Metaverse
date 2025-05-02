using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyPlaneUIManager : MonoBehaviour
{
    public TextMeshProUGUI FlappyPlanescoreText;
    public TextMeshProUGUI FlappyPlanerestartText;

    // Start is called before the first frame update
    void Start()
    {
        if (FlappyPlanerestartText == null)
            Debug.LogError("restart text is null");

        if (FlappyPlanescoreText == null)
            Debug.LogError("score text is null");

        FlappyPlanerestartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        FlappyPlanerestartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        FlappyPlanescoreText.text = score.ToString();
    }
}
