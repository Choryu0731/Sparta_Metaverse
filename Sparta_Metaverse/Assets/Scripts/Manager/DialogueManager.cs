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
            talkText.text = "�̰��� �̸��� " + scanObj.name + "�̶�� �Ѵ�.";
        }
        else
        {
            Debug.LogWarning("Talk Text �Ǵ� Scan Object�� null�Դϴ�.");
        }
    }
}
