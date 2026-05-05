using UnityEngine;

public class RemoveItemEffect : IEffect
{
    private string itemID;
    private int amount;

    public RemoveItemEffect(EffectData data)
    {
        itemID = data.targetID;
        amount = data.amount;
    }

    public void Execute()
    {
        InventoryController.Instance.RemoveItem(itemID, amount);
    }
}
