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
            steps = new List<QuestStep>(quest.steps),
            currentStepIndex = 0,
            currentStepAmount = 0
        };

        activeQuests.Add(questStatus);

        questStatus.steps[0].OnStart(questStatus);
    }

    public void AdvanceStep(QuestStatus status)
    {
        QuestStep currentStep = status.steps[status.currentStepIndex];
        currentStep.OnFinish();

        status.currentStepIndex++;
        status.currentStepAmount = 0;

        if (status.currentStepIndex >= status.steps.Count)
        {
            CompleteQuest(status);
            return;
        }

        QuestStep nextStep = status.steps[status.currentStepIndex];
        nextStep.OnStart(status);
    }

    private void CompleteQuest(QuestStatus status)
    {
        status.state = QuestState.Completed;

        activeQuests.Remove(status);
        completedQuests.Add(status);

        Debug.Log("任务完成：" + status.questID);
    }
}
