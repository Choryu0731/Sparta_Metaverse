using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    private Queue<string> dialogueLines;
    private Action onDialogueEnd;
    private Action onDialogueFinished; // ��ȭ�� ������ ����Ǿ����� �˸��� Action

    private void Awake()
    {
        gameObject.SetActive(false);
        dialogueLines = new Queue<string>();
    }

    public void StartDialogue(string[] lines, Action onEnd)
    {
        dialogueLines.Clear();
        foreach (string line in lines)
        {
            dialogueLines.Enqueue(line);
        }
        onDialogueEnd = onEnd;
        onDialogueFinished = null; // StartDialogue �� �ݹ� �ʱ�ȭ
        DisplayNextLine();
    }

    public void SetDialogueFinishedCallback(Action callback)
    {
        onDialogueFinished = callback;
    }

    public void DisplayNextLine()
    {
        if (dialogueLines.Count > 0)
        {
            talkText.text = dialogueLines.Dequeue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        gameObject.SetActive(false);
        onDialogueEnd?.Invoke();
        onDialogueFinished?.Invoke(); // ��ȭ ���� �ݹ� ����
    }
}