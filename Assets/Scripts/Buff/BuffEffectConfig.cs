using UnityEngine;

public enum EffectType
{ 
    ModifyStat = 0,
    Heal = 1,
    Stun = 2,
    Invincible = 3
    //...
}

[System.Serializable]
public class BuffEffectConfig
{
    public EffectType type;

    //ModifyStat
    public StatType stat;
    public StatModType mode;

    public float value;

    public float interval;
}
