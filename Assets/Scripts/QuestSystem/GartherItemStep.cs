using UnityEngine;

[System.Serializable]
public class GartherItemStep : QuestStep
{
    public string itemID;
    public int totalAmount;

    private QuestStatus status;

    public override void OnStart(QuestStatus questStatus)
    {
        status = questStatus;
        EventCenter.Subscribe<GartherItemEvent>(OnGartherItem);
    }

    public override void OnFinish()
    {
        EventCenter.Unsubscribe<GartherItemEvent>(OnGartherItem);
    }

    public override string GetProgressText()
    {
        return $"{stepDescription} ({status.currentStepAmount}/{totalAmount})";
    }

    private void OnGartherItem(GartherItemEvent e)
    {
        if (e.itemID != itemID) return;

        status.currentStepAmount += e.amount;
        QuestManager.Instance.trackerUI.Refresh();

        if (status.currentStepAmount >= totalAmount)
        {
            QuestManager.Instance.AdvanceStep(status);
        }
    }
}
