using TMPro;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        QuestRuntime quest = QuestManager.Instance.selectedQuest;

        if (quest == null)
        {
            questName.text = "";
            questDescription.text = "";
            return;
        }

        questName.text = quest.data.questName;
        questDescription.text = quest.CurrentStep.GetDescription();
    }
}
