using System;
using UnityEngine;

public class TalkCondition : ICondition
{
    private string npcID;
    private bool done;

    private Action onComplete;

    public TalkCondition(ConditionData data)
    {
        npcID = data.targetID;
    }

    public void Register(Action callback)
    {
        onComplete = callback;
        EventCenter.Subscribe<TalkToNPCEvent>(OnTalk);
    }

    public void Unregister()
    {
        EventCenter.Unsubscribe<TalkToNPCEvent>(OnTalk);
    }

    private void OnTalk(TalkToNPCEvent e)
    {
        if (e.npcID != npcID) return;

        done = true;
        onComplete?.Invoke();
    }

    public bool IsMet()
    {
        return done;
    }
}
