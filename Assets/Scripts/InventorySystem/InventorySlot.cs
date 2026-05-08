using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI count;
    public GameObject highlight;

    private ItemData data;

    public void SetItem(ItemData item)
    {
        data = item;
        
        if (data == null)
        {
            icon.enabled = false;
            count.enabled = false;
            return;
        }

        ItemConfig config = ItemDatabase.Instance.Get(data.itemID);

        icon.enabled = true;
        icon.sprite = config.icon;

        count.enabled = true;
        count.text = data.amount.ToString();
    }

    public ItemData GetItem()
    {
        return data;
    }

    public void SetSelected(bool selected)
    {
        highlight.SetActive(selected);
    }
}
