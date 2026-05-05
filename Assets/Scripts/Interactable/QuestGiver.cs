using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public string npcID;

    public string normalDialogueID;

    public List<string> questSequence;

    private int index = 0;

    public void Interact()
    {
        var quest = QuestManager.Instance.selectedQuest;

        //1.满足任务步骤中的对话条件
        if (quest != null && !quest.IsCompleted)
        {
            var step = quest.CurrentStep;

            if (step.HasTalkCondition(npcID))
            {
                string dialogueID = step.GetDialogueID();

                DialogueManager.Instance.StartDialogue(dialogueID, () =>
                {
                    EventCenter.Publish(new TalkToNPCEvent
                    {
                        npcID = this.npcID,
                    });
                });

                return;
            }
        }

        //2.接受任务
        if (index < questSequence.Count)
        {
            var data = QuestDatabase.Instance.Get(questSequence[index]);
            DialogueManager.Instance.StartDialogue(data.giveDialogueID, () =>
            {
                QuestManager.Instance.AcceptQuest(data);
            });
            index++;

            return;
        }

        //3.其它情况
        DialogueManager.Instance.StartDialogue(normalDialogueID);
    }
}
