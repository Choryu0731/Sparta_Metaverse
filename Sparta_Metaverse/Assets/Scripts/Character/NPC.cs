using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionState
{
    Dialogue,
    UIOn,
    UIOff
}

public enum NPCType
{
    MiniGame1,
    MiniGame2,
    VehicleChange,
    Customizing,
    Leaderboard,
    MapGuide,
    Common
}

public class NPC : MonoBehaviour
{
    public NPCType npcType;
    private bool isInteracting = false;
    private InteractionState currentState = InteractionState.UIOff; // �ʱ� ���´� UI Off

    public DialogueManager dialogueManager;
    public PlayerController playerController;
    public string[] dialogue; // �� NPC�� ���
    public GameObject uiToShowOnDialogueEnd; // ��ȭ ���� �� ǥ���� UI (Inspector���� ����)

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        if (currentState == InteractionState.UIOff)
        {
            // UI�� ���� ���� -> ��ȭâ �ѱ�
            if (dialogueManager != null && playerController != null && playerController.ScanObject != null)
            {
                dialogueManager.scanObject = playerController.ScanObject;
                dialogueManager.StartDialogue(dialogue, () =>
                {
                    // ��ȭ�� ���� �� UI�� �Ѵ� ������ �״�� ����
                    if (uiToShowOnDialogueEnd != null)
                    {
                        uiToShowOnDialogueEnd.SetActive(true);
                    }
                });
                dialogueManager.SetDialogueFinishedCallback(() => currentState = InteractionState.UIOn); // ��ȭ ���� �� ���� ���� �ݹ� ���
                dialogueManager.gameObject.SetActive(true);
                currentState = InteractionState.Dialogue;
            }
        }
        else if (currentState == InteractionState.Dialogue)
        {
            // ��ȭâ�� ���� ���� -> ��ȭâ ����
            if (dialogueManager != null && dialogueManager.gameObject.activeSelf)
            {
                dialogueManager.gameObject.SetActive(false);
            }
            currentState = InteractionState.UIOn; // ���� UIOn ���·� ����
            if (uiToShowOnDialogueEnd != null && !uiToShowOnDialogueEnd.activeSelf)
            {
                uiToShowOnDialogueEnd.SetActive(true);
            }
        }
        else if (currentState == InteractionState.UIOn)
        {
            // UI�� ���� ���� -> UI ����
            if (uiToShowOnDialogueEnd != null)
            {
                uiToShowOnDialogueEnd.SetActive(false);
            }
            currentState = InteractionState.UIOff;
        }

        StartCoroutine(ResetInteractionFlag());
    }

    private IEnumerator ResetInteractionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
