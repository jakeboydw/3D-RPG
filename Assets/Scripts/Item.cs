using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID;
    public EventCenter eventCenter;

    private void OnEnable()
    {
        if (eventCenter)
        {
            eventCenter.Interact += AddToInventory;
        }
    }

    private void OnDisable()
    {
        if (eventCenter)
        {
            eventCenter.Interact -= AddToInventory;
        }
    }

    private void AddToInventory()
    {
        Inventory.Instance.Add(this);
        Destroy(this.gameObject);
    }
}
