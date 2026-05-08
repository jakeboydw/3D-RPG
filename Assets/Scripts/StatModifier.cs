using UnityEngine;

public enum StatType
{ 
    MaxHP,
    Attack,
    MoveSpeed
}

public enum StatModType
{
    Flat,     // +20
    Percent   // +10%
}

public class StatModifier
{
    public StatType statType;
    public StatModType modType;
    public float value;
    public object source;  //Buff、装备、技能等
}
