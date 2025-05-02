using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private NPC npc;

    private void Awake()
    {
        npc = GetComponent<NPC>();
    }

    public void Trigger()
    {
        Debug.Log($"[InteractionTrigger] {gameObject.name}�� ��ȣ�ۿ� �õ�");

        if (npc != null)
        {
            Debug.Log("��ȣ�ۿ� ����");
            npc.Interact();
        }
        else
        {
            Debug.Log("npc ��ũ��Ʈ�� ����");
        }
    }
}
