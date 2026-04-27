using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public GameObject highlight;

    private ItemData data;

    public void SetItem(ItemData item)
    {
        data = item;

        if (data == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.enabled = true;
            //set icon
        }
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
