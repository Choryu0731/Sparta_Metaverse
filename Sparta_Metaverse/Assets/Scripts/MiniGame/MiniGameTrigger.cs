using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public string miniGameScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MiniGameManager.Instance.StartMiniGame(miniGameScene);
        }
    }
}
