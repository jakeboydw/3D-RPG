using UnityEngine;

public enum StatType
{ 
    MaxHP = 0,
    Attack = 1,
    MoveSpeed = 2
}

public enum StatModType
{
    Flat = 0,     // +20
    Percent = 1   // +10%
}

public class StatModifier
{
    public StatType statType;
    public StatModType modType;
    public float value;
    public object source;  //Buff、装备、技能等
}
