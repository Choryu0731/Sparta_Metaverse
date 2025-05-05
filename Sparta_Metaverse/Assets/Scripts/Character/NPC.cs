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

        if (isDialogueActive) // ��ȭâ�� Ȱ��ȭ�Ǿ� �ִٸ� �ݱ�
        {
            if (dialoguePanel != null && dialoguePanel.gameObject.activeSelf)
            {
                dialoguePanel.gameObject.SetActive(false); // DialogueManager ������Ʈ ��Ȱ��ȭ
            }
            isDialogueActive = false;
        }
        else // ��ȭâ�� ��Ȱ��ȭ�Ǿ� �ִٸ� ����
        {
            StartCoroutine(HandleInteraction());
            isDialogueActive = true;
        }

        StartCoroutine(ResetInteractionFlag()); // ��ȣ�ۿ� �÷��� �ʱ�ȭ �ڷ�ƾ ����
    }

    private IEnumerator HandleInteraction()
    {
        switch (npcType)
        {
            case NPCType.MiniGame1:
                if (dialoguePanel != null && playerController != null && playerController.ScanObject != null)
                {
                    dialoguePanel.GetComponent<DialogueManager>().Action(playerController.ScanObject);
                    dialoguePanel.gameObject.SetActive(true); // DialogueManager ������Ʈ Ȱ��ȭ
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
                UIManager.Instance.OpenMapGuide("���ĸ�Ÿ ��Ÿ������ ���Ű� ȯ���մϴ�!");
                break;
        }

        yield return null; // HandleInteraction�� UI Ȱ��ȭ�� ����ϰ� �ٷ� ����
    }

    private IEnumerator ResetInteractionFlag()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
