using UnityEngine;

public class ModifyStatEffect : IBuffEffect
{
    BuffEffectConfig config;

    public ModifyStatEffect(BuffEffectConfig config)
    {
        this.config = config;
    }

    public void OnApply(BuffRuntime buff)
    {
        var stats = buff.owner.GetComponent<Character>().Stats;
        stats.GetStat(config.stat).AddModifier(new StatModifier
        {
            statType = config.stat,
            modType = config.mode,
            value = config.value,
            source = this
        });
    }

    public void OnRemove(BuffRuntime buff)
    {
        var stats = buff.owner.GetComponent<Character>().Stats;
        stats.GetStat(config.stat).RemoveModifier(this);
    }
}
