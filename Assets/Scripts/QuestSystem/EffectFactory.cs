using UnityEngine;

public static class EffectFactory
{
    public static IEffect Create(EffectData data)
    {
        return data.type switch
        {
            "AddItem" => new AddItemEffect(data),
            "RemoveItem" => new RemoveItemEffect(data),
            _ => null
        };
    }
}
