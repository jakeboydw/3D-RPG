using UnityEngine;

public static class EffectFactory
{
    public static IEffect Create(EffectData data)
    {
        return data.type switch
        {
            "RemoveItem" => new RemoveItemEffect(data),
            _ => null
        };
    }
}
