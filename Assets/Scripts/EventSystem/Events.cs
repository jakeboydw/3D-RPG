public struct CollectItemEvent
{
    public string itemID;
    public int amount;
}

public struct KillEnemyEvent
{
    public string enemyID;
}

public struct TalkToNPCEvent
{
    public string npcID;
}