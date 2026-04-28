using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public GameObject panel;

    public GameObject slotPrefab;
    public Transform gridRoot;
    public int slotCount = 15;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        GenerateSlots();
    }

    //自动生成物品栏
    private void GenerateSlots()
    {
        slots.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, gridRoot);
            InventorySlot slot = newSlot.GetComponent<InventorySlot>();
            slots.Add(slot);
        }
    }

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

    public int ClampIndex(int index)
    {
        return Mathf.Clamp(index, 0, slots.Count - 1);
    }
}
