using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f; // 장애물 상하 이동 지정

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f; // top, bottom 사이에 공간

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f; // 장애물 사이의 폭

    FlappyPlaneGameManager FlappyPlanegameManager;

    private void Start()
    {
        FlappyPlanegameManager = FlappyPlaneGameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlappyPlanePlayer player = collision.GetComponent<FlappyPlanePlayer>();
        if (player != null)
            FlappyPlanegameManager.AddScore(1);
    }
}
