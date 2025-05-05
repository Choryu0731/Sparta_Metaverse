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
    public string[] dialogue; // �� NPC�� ���

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        if (isDialogueActive) // ��ȭâ�� Ȱ��ȭ�Ǿ� �ִٸ� �ݱ�
        {
            if (dialogueManager != null && dialogueManager.gameObject.activeSelf)
            {
                dialogueManager.gameObject.SetActive(false);
            }
            isDialogueActive = false;
        }
        else // ��ȭâ�� ��Ȱ��ȭ�Ǿ� �ִٸ� ����
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
                UIManager.Instance.OpenMapGuide("���ĸ�Ÿ ��Ÿ������ ���Ű� ȯ���մϴ�!");
                break;
            case NPCType.Common:
                // ���� NPC�� Ư���� UI�� ���� ���� ���� �ֽ��ϴ�.
                break;
        }
    }

    private IEnumerator ResetInteractionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
