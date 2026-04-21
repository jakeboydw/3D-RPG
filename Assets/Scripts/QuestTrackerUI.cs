using TMPro;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    public TextMeshPro questName;
    public TextMeshPro questDescription;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        QuestStatus quest = QuestManager.Instance.selectedQuest;

        if (quest == null)
        {
            questName.text = "";
            questDescription.text = "";
            return;
        }

        QuestStep questStep = quest.steps[quest.currentStepIndex];
        questName.text = quest.questName;
        questDescription.text = questStep.GetProgressText();
    }
}
