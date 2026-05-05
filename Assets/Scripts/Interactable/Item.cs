using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemID;

    public void Interact()
    {
        AddToInventory();
    }

    private void AddToInventory()
    {
        InventoryController.Instance.AddItem(itemID, 1);
        Destroy(gameObject);
    }
}
