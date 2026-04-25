using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    public string questID;
    public string questName;
    public QuestState state;

    [System.NonSerialized]
    public List<QuestStep> steps; //无法显示多态引用，避免在Inspector中序列化

    public int currentStepIndex;
    public int currentStepAmount;
}

public enum QuestState
{
    InProgress,
    Completed
}
