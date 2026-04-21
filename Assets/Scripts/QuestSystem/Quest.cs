using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "RPG/Quest")]
public class Quest : ScriptableObject
{
    [Header("基础信息")]
    public string questID; //仅用于区分不同任务
    public string questName;
    [TextArea]
    public string description;

    [Header("任务步骤"), SerializeReference]
    public List<QuestStep> steps = new List<QuestStep>();
}
