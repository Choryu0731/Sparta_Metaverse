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
        Debug.Log($"[InteractionTrigger] {gameObject.name}와 상호작용 시도");

        if (npc != null)
        {
            Debug.Log("상호작용 실행");
            npc.Interact();
        }
        else
        {
            Debug.Log("npc 스크립트가 없음");
        }
    }
}
