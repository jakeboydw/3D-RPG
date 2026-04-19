using UnityEngine;

[System.Serializable]
public class KillEnemyStep : QuestStep
{
    public string enemyID;
    public int totalAmount;

    private QuestStatus status;

    public override void OnStart(QuestStatus questStatus)
    {
        status = questStatus;
        EventCenter.Subscribe<KillEnemyEvent>(OnKillEnemy);
    }

    public override void OnFinish()
    {
        EventCenter.Unsubscribe<KillEnemyEvent>(OnKillEnemy);
    }

    private void OnKillEnemy(KillEnemyEvent e)
    {
        if (e.enemyID != enemyID) return;

        status.currentStepAmount++;
        if (status.currentStepAmount >= totalAmount)
        {
            QuestManager.Instance.AdvanceStep(status);
        }
    }
}
