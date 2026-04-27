using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public GameObject panel;
    public List<InventorySlot> slots;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    public void Show(bool show)
    {
        panel.SetActive(show);
    }

    public void Refresh(IReadOnlyList<ItemData> items)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].SetItem(null);
            }
        }
    }

    public void SetSelected(int index)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetSelected(i == index);
        }
    }

    public void SetDescription(ItemData item)
    {
        if (item == null)
        {
            itemName.text = "";
            itemDescription.text = "";
            return;
        }

        ItemConfig config = ItemDatabase.Instance.Get(item.itemID);

        itemName.text = config.itemName;
        itemDescription.text = config.description;
    }

    public Transform GetSlotTransform(int index)
    {
        return slots[index].transform;
    }
}
