using UnityEngine;

public class AddItemEffect : IEffect
{
    private string itemID;
    private int amount;

    public AddItemEffect(EffectData data)
    {
        itemID = data.targetID;
        amount = data.amount;
    }

    public void Execute()
    {
        InventoryController.Instance.AddItem(itemID, amount);
    }
}
