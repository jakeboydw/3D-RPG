using UnityEngine;

[System.Serializable]
public class TalkToNPCStep : QuestStep
{
    public string npcID;
    public DialogueData stepDialogue;

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

        //QuestStep只负责推进，不负责表现，由NPC(QuestGiver)展示stepDialogue，实现任务和对话的解耦
        QuestManager.Instance.AdvanceStep(status);
    }
}
