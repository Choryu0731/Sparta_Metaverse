using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isDialogueActive = false;

    public DialogueManager dialogueManager;
    public PlayerController playerController;
    public string[] dialogue; // 각 NPC별 대사

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        if (isDialogueActive) // 대화창이 활성화되어 있다면 닫기
        {
            if (dialogueManager != null && dialogueManager.gameObject.activeSelf)
            {
                dialogueManager.gameObject.SetActive(false);
            }
            isDialogueActive = false;
        }
        else // 대화창이 비활성화되어 있다면 열기
        {
            if (dialogueManager != null && playerController != null && playerController.ScanObject != null)
            {
                dialogueManager.scanObject =  playerController.ScanObject;
                dialogueManager.StartDialogue(dialogue, () => OpenSpecificUI(npcType));
                dialogueManager.gameObject.SetActive(true);
                isDialogueActive = true;
            }
        }

        StartCoroutine(ResetInteractionFlag());
    }

    private void OpenSpecificUI(NPCType type)
    {
        switch (type)
        {
            case NPCType.MiniGame1:
                MiniGameManager.Instance.StartMiniGame("MiniGame_FlappyPlane");
                break;
            case NPCType.MiniGame2:
                MiniGameManager.Instance.StartMiniGame("MiniGame2Scene");
                break;
            case NPCType.VehicleChange:
                UIManager.Instance.OpenVehicleUI();
                break;
            case NPCType.Customizing:
                UIManager.Instance.OpenCustomizeUI();
                break;
            case NPCType.Leaderboard:
                UIManager.Instance.OpenLeaderboard();
                break;
            case NPCType.MapGuide:
                UIManager.Instance.OpenMapGuide("스파르타 메타버스에 오신걸 환영합니다!");
                break;
            case NPCType.Common:
                // 공통 NPC는 특별한 UI를 열지 않을 수도 있습니다.
                break;
        }
    }

    private IEnumerator ResetInteractionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
