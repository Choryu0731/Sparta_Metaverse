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
        npc.Interact();
    }
}
