using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance => instance;

    public List<QuestStatus> activeQuests = new List<QuestStatus>();
    public List<QuestStatus> completedQuests = new List<QuestStatus>();
    public QuestStatus selectedQuest; //当前追踪的任务

    private void Awake()
    {
        //任务管理器单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void AcceptQuest(Quest quest)
    {
        QuestStatus questStatus = new QuestStatus
        {
            questID = quest.questName,
            state = QuestState.InProgress,
            steps = quest.steps,
            currentStepIndex = 0,
            currentStepAmount = 0
        };

        activeQuests.Add(questStatus);
    }
}
