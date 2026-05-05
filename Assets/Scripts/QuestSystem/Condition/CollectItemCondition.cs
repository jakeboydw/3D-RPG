using System;
using UnityEngine;

public class CollectItemCondition : ICondition
{
    private string itemID;
    private int required;
    private int current;

    private Action onComplete;

    public CollectItemCondition(ConditionData data)
    {
        itemID = data.targetID;
        required = data.amount;
        current = InventoryController.Instance.Count(itemID);
    }

    public void Register(Action callback)
    {
        onComplete = callback;
        EventCenter.Subscribe<CollectItemEvent>(OnCollectItem);
    }

    public void Unregister()
    {
        EventCenter.Unsubscribe<CollectItemEvent>(OnCollectItem);
    }

    private void OnCollectItem(CollectItemEvent e)
    {
        if (e.itemID != itemID) return;

        current += e.amount;
        if (current >= required)
        {
            onComplete?.Invoke();
        }
    }

    public bool IsMet()
    {
        return current >= required;
    }
}
