using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public List<Quest> quests;

    private int availableIndex = 0;
    private Quest availableQuest;

    private void Start()
    {
        NextQuest();
    }

    public void Interact(PlayerAction player)
    {

    }

    private void GiveQuest()
    {
        if (availableQuest)
        {
            QuestManager.Instance.AcceptQuest(availableQuest);
            availableIndex++;
            NextQuest();
        }
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
