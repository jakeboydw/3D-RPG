using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance => instance;

    public List<QuestRuntime> activeQuests = new List<QuestRuntime>();
    public List<QuestRuntime> completedQuests = new List<QuestRuntime>();

    public QuestRuntime selectedQuest; //当前追踪的任务

    public QuestTrackerUI trackerUI;

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

    public void AcceptQuest(QuestData data)
    {
        QuestRuntime runtime = new QuestRuntime(data);

        activeQuests.Add(runtime);
        if (selectedQuest == null)
        {
            selectedQuest = runtime;
        }

        RefreshUI();
    }

    public void OnQuestCompleted(QuestRuntime quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        if (selectedQuest == quest)
        {
            selectedQuest = activeQuests.Count > 0 ? activeQuests[^1] : null;
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (trackerUI != null)
        {
            trackerUI.Refresh();
        }
    }
}
