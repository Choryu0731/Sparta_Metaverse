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

    public void Interact()
    {
        if (isInteracting) return;
        isInteracting = true;

        StartCoroutine(HandleInteraction());
        Debug.Log("NPC와 상호작용 시작: " + npcType);
    }

    private IEnumerator HandleInteraction()
    {
        switch (npcType)
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
        }

        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}
