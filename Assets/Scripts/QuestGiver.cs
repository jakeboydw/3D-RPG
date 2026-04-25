using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public string npcID;
    public List<Quest> quests;

    public DialogueData normalDialogue;

    private int availableIndex = 0;
    private Quest availableQuest;

    private void Start()
    {
        NextQuest();
    }

    public void Interact()
    {
        QuestStatus quest = QuestManager.Instance.selectedQuest;

        if (quest != null)
        {
            QuestStep step = quest.steps[quest.currentStepIndex];

            if (step is TalkToNPCStep talkStep && talkStep.npcID == this.npcID)
            {
                DialogueManager.Instance.StartDialogue(talkStep.stepDialogue, () => EventCenter.Publish(new TalkToNPCEvent
                {
                    npcID = this.npcID,
                }));
                return;
            }

            DialogueManager.Instance.StartDialogue(normalDialogue);
            return;
        }

        if (availableQuest != null)
        {
            DialogueManager.Instance.StartDialogue(availableQuest.giveQuestDialogue, GiveQuest);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(normalDialogue);
        }
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
