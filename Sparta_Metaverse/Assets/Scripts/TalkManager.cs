using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(6000, new string[] { "안녕?", "이 곳에 처음 왔구나?" });
    }

    public string GetTalk(int id, int talkIndex)
    {

    }
}
