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
    private enum InteractionStep { Dialogue, UIOn, UIOff }
    private InteractionStep currentStep = InteractionStep.UIOff;

    public DialogueManager dialogueManager;
    public PlayerController playerController;
    public string[] dialogue;
    public GameObject uiToShowOnDialogueEnd;

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        if (currentStep == InteractionStep.UIOff)
        {
            // UI가 꺼진 상태 -> 대화창 켜기
            if (dialogueManager != null && playerController != null && playerController.ScanObject != null)
            {
                dialogueManager.scanObject = playerController.ScanObject;
                dialogueManager.StartDialogue(dialogue, () =>
                {
                    // 대화 종료 후 UIOn 상태로 변경하고, npcType에 따라 미니게임 UI를 열거나 일반 UI를 켭니다.
                    currentStep = InteractionStep.UIOn;
                    if (npcType == NPCType.MiniGame1)
                    {
                        UIManager.Instance.OpenMiniGameConfirmUI("MiniGame_FlappyPlane");
                    }
                    else if (npcType == NPCType.MiniGame2)
                    {
                        UIManager.Instance.OpenMiniGameConfirmUI("MiniGame2Scene");
                    }
                    else if (uiToShowOnDialogueEnd != null)
                    {
                        uiToShowOnDialogueEnd.SetActive(true);
                    }
                });
                dialogueManager.gameObject.SetActive(true);
                currentStep = InteractionStep.Dialogue;
            }
        }
        else if (currentStep == InteractionStep.Dialogue)
        {
            // 대화창이 켜진 상태 -> 대화창 끄기, 다음 단계로
            if (dialogueManager != null && dialogueManager.gameObject.activeSelf)
            {
                dialogueManager.gameObject.SetActive(false);
            }
            currentStep = InteractionStep.UIOn;
            // 대화 종료 시 미니게임 UI는 콜백에서 처리했으므로, 일반 UI만 처리합니다.
            if (npcType != NPCType.MiniGame1 && npcType != NPCType.MiniGame2 && uiToShowOnDialogueEnd != null && !uiToShowOnDialogueEnd.activeSelf)
            {
                uiToShowOnDialogueEnd.SetActive(true);
            }
        }
        else if (currentStep == InteractionStep.UIOn)
        {
            // UI가 켜진 상태 -> UI 끄기, 다음 단계로
            if (uiToShowOnDialogueEnd != null)
            {
                uiToShowOnDialogueEnd.SetActive(false);
            }
            currentStep = InteractionStep.UIOff;
        }

        StartCoroutine(ResetInteractionFlag());
    }

    private IEnumerator ResetInteractionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
