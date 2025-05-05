using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;
    public GameObject scanObject;

    private void Awake()
    {
        
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        if (talkText != null && scanObj != null)
        {
            talkText.text = "이것의 이름은 " + scanObj.name + "이라고 한다.";
        }
        else
        {
            Debug.LogWarning("Talk Text 또는 Scan Object가 null입니다.");
        }
    }
}
