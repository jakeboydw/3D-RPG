using System.Collections.Generic;
using UnityEngine;

public class StepRuntime
{
    private StepData data;
    private QuestRuntime quest;

    private List<ICondition> conditions = new();
    private List<IEffect> onStartEffects = new();
    private List<IEffect> onFinishEffects = new();

    public StepRuntime(StepData data, QuestRuntime quest)
    {
        this.data = data;
        this.quest = quest;

        foreach (var c in data.conditions)
        {
            conditions.Add(ConditionFactory.Create(c, this));
        }

        foreach (var e in data.onStartEffects)
        {
            onStartEffects.Add(EffectFactory.Create(e));
        }

        foreach (var e in data.onFinishEffects)
        {
            onFinishEffects.Add(EffectFactory.Create(e));
        }
    }

    public void OnStart()
    {
        foreach (var e in onStartEffects)
        {
            e.Execute();
        }

        foreach (var c in conditions)
        {
            c.Register(CheckComplete);
        }
    }

    public void OnFinish()
    {
        foreach (var e in onFinishEffects)
        {
            e.Execute();
        }

        foreach (var c in conditions)
        {
            c.Unregister();
        }
    }

    public bool IsComplete()
    {
        foreach (var c in conditions)
        {
            if (!c.IsMet()) return false;
        }
        return true;
    }

    private void CheckComplete()
    {
        if (IsComplete())
        {
            quest.Advance();
        }
    }

    public string GetDescription()
    {
        return data.description;
    }

    public string GetDialogueID()
    {
        return data.dialogueID;
    }

    public bool HasTalkCondition(string npcID)
    {
        foreach (var c in data.conditions)
        {
            if (c.type == "TalkToNPC" && c.targetID == npcID)
            {
                return true;
            }
        }

        return false;
    }
}
