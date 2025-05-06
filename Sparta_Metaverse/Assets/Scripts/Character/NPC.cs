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
    private InteractionState currentState = InteractionState.UIOff; // 초기 상태는 UI Off

    public DialogueManager dialogueManager;
    public PlayerController playerController;
    public string[] dialogue; // 각 NPC별 대사
    public GameObject uiToShowOnDialogueEnd; // 대화 종료 후 표시할 UI (Inspector에서 연결)

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        if (currentState == InteractionState.UIOff)
        {
            // UI가 꺼진 상태 -> 대화창 켜기
            if (dialogueManager != null && playerController != null && playerController.ScanObject != null)
            {
                dialogueManager.scanObject = playerController.ScanObject;
                dialogueManager.StartDialogue(dialogue, () =>
                {
                    // 대화가 끝난 후 UI를 켜는 로직은 그대로 유지
                    if (uiToShowOnDialogueEnd != null)
                    {
                        uiToShowOnDialogueEnd.SetActive(true);
                    }
                });
                dialogueManager.SetDialogueFinishedCallback(() => currentState = InteractionState.UIOn); // 대화 종료 시 상태 변경 콜백 등록
                dialogueManager.gameObject.SetActive(true);
                currentState = InteractionState.Dialogue;
            }
        }
        else if (currentState == InteractionState.Dialogue)
        {
            // 대화창이 켜진 상태 -> 대화창 끄기
            if (dialogueManager != null && dialogueManager.gameObject.activeSelf)
            {
                dialogueManager.gameObject.SetActive(false);
            }
            currentState = InteractionState.UIOn; // 직접 UIOn 상태로 변경
            if (uiToShowOnDialogueEnd != null && !uiToShowOnDialogueEnd.activeSelf)
            {
                uiToShowOnDialogueEnd.SetActive(true);
            }
        }
        else if (currentState == InteractionState.UIOn)
        {
            // UI가 켜진 상태 -> UI 끄기
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
