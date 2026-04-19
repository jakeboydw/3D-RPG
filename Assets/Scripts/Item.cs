using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemID;

    public void Interact(PlayerAction player)
    {
        AddToInventory();
    }

    private void AddToInventory()
    {
        Inventory.Instance.Add(this);
        Destroy(this.gameObject);
    }
}
