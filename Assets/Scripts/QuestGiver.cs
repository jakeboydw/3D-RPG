using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public EventCenter eventCenter;
    public List<Quest> quests;

    private int availableIndex = 0;
    private Quest availableQuest;

    private void Start()
    {
        NextQuest();
    }

    private void OnEnable()
    {
        if (eventCenter)
        {
            eventCenter.Interact += GiveQuest;
        }
    }

    private void OnDisable()
    {
        if (eventCenter)
        {
            eventCenter.Interact -= GiveQuest;
        }
    }

    private void GiveQuest()
    {
        QuestManager.Instance.AcceptQuest(availableQuest);
        availableIndex++;
        NextQuest();
    }

    private void NextQuest()
    {
        if (availableIndex < quests.Count)
        {
            availableQuest = quests[availableIndex];
        }
        else
        {
            availableQuest = null;
        }
    }
}
