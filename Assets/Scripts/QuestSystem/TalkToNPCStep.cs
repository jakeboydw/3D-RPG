using UnityEngine;

[System.Serializable]
public class TalkToNPCStep : QuestStep
{
    public string npcID;

    private QuestStatus status;

    public override void OnStart(QuestStatus questStatus)
    {
        status = questStatus;
        EventCenter.Subscribe<TalkToNPCEvent>(OnTalk);
    }

    public override void OnFinish()
    {
        EventCenter.Unsubscribe<TalkToNPCEvent>(OnTalk);
    }

    public override string GetProgressText()
    {
        return stepDescription;
    }

    private void OnTalk(TalkToNPCEvent e)
    {
        if (e.npcID != npcID) return;

        QuestManager.Instance.AdvanceStep(status);
    }
}
