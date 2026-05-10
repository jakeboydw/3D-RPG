using UnityEngine;

public static class BuffEffectFactory
{
    public static IBuffEffect Create(BuffEffectConfig config)
    {
        return config.type switch
        {
            EffectType.ModifyStat => new ModifyStatEffect(config),
            EffectType.Heal => new HealEffect(config),
            _ => null
        };
    }
}
