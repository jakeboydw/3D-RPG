using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    public string questID;
    public string questName;
    public QuestState state;
    public List<QuestStep> steps;
    public int currentStepIndex;
    public int currentStepAmount;
}

public enum QuestState
{
    InProgress,
    Completed
}
