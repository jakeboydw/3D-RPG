using UnityEngine;

public class HealEffect : IBuffEffect
{
    public BuffEffectConfig config;

    public HealEffect(BuffEffectConfig config)
    {
        this.config = config;
    }

    public void OnApply(BuffRuntime buff)
    {
        var health = buff.owner.GetComponent<Health>();
        health.Heal(config.value);
    }

    public void OnRemove(BuffRuntime buff)
    {

    }
}
