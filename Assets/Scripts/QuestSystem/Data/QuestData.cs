using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string questID;
    public string questName;
    public string description;

    public string giveDialogueID;

    public List<StepData> steps;
}
