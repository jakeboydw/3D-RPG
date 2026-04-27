using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemID;
    public int amount;
}

public class InventoryModel
{
    private List<ItemData> items = new();

    public IReadOnlyList<ItemData> Items => items;

    public event Action onChanged;

    public void Add(string itemID, int amount = 1)
    {
        ItemData item = items.Find(i => i.itemID == itemID);

        if (item != null)
        {
            item.amount += amount;
        }
        else
        {
            items.Add(new ItemData
            {
                itemID = itemID,
                amount = amount
            });
        }

        onChanged?.Invoke();
    }

    public void Remove(ItemData item)
    {
        items.Remove(item);
        onChanged?.Invoke();
    }
}
