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
    MapGuide
}

public class NPC : MonoBehaviour
{
    public NPCType npcType;
    private bool isInteracting = false;
    private bool isDialogueActive = false;

    public GameObject dialoguePanel;
    public PlayerController playerController;

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        if (isDialogueActive) // 대화창이 활성화되어 있다면 닫기
        {
            if (dialoguePanel != null && dialoguePanel.gameObject.activeSelf)
            {
                dialoguePanel.gameObject.SetActive(false); // DialogueManager 오브젝트 비활성화
            }
            isDialogueActive = false;
        }
        else // 대화창이 비활성화되어 있다면 열기
        {
            StartCoroutine(HandleInteraction());
            isDialogueActive = true;
        }

        StartCoroutine(ResetInteractionFlag()); // 상호작용 플래그 초기화 코루틴 시작
    }

    private IEnumerator HandleInteraction()
    {
        switch (npcType)
        {
            case NPCType.MiniGame1:
                if (dialoguePanel != null && playerController != null && playerController.ScanObject != null)
                {
                    dialoguePanel.GetComponent<DialogueManager>().Action(playerController.ScanObject);
                    dialoguePanel.gameObject.SetActive(true); // DialogueManager 오브젝트 활성화
                }
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
        }

        yield return null; // HandleInteraction은 UI 활성화만 담당하고 바로 종료
    }

    private IEnumerator ResetInteractionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
