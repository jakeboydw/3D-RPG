using System.Collections.Generic;
using UnityEngine;

public class QuestRuntime
{
    public QuestData data;

    public int currentStepIndex;
    public List<StepRuntime> steps;

    public bool IsCompleted => currentStepIndex >= steps.Count;

    public StepRuntime CurrentStep => steps[currentStepIndex];

    public QuestRuntime(QuestData data)
    {
        this.data = data;

        steps = new List<StepRuntime>();
        foreach (var stepData in data.steps)
        {
            steps.Add(new StepRuntime(stepData, this));
        }

        //自动跳过已完成步骤
        while (!IsCompleted && CurrentStep.IsComplete())
        {
            currentStepIndex++;
        }

        StartCurrentStep();
    }

    public void StartCurrentStep()
    {
        if (IsCompleted) return;
        CurrentStep.OnStart();

        QuestManager.Instance.RefreshUI();
    }

    public void Advance()
    {
        CurrentStep.OnFinish();
        currentStepIndex++;
        if (!IsCompleted)
        {
            StartCurrentStep();
        }
        else
        {
            QuestManager.Instance.OnQuestCompleted(this);
        }
    }
}
