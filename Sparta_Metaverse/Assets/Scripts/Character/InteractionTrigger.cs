using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private NPC npc;

    private void Awake()
    {
        npc = GetComponent<NPC>();
        Debug.Log(gameObject.name + ": NPC component = " + npc);
    }

    public void Trigger()
    {
        Debug.Log(gameObject.name + ": Triggered!");
        if (npc != null)
        {
            npc.Interact();
            Debug.Log(gameObject.name + ": NPC Interact() called on " + npc.gameObject.name);
        }
    }
}
