using UnityEngine;

[System.Serializable]
public class QuestStep
{
    public string stepDescription;
}

[System.Serializable]
public class GartherItemStep : QuestStep
{
    public string itemID;
    public int totalAmount;
}

[System.Serializable]
public class TalkToNPCStep : QuestStep
{
    public string npcID;
}

[System.Serializable]
public class KillEnemyStep : QuestStep
{
    public string enemyID;
    public int totalAmount;
}