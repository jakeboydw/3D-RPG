using UnityEngine;

public enum BuffTarget
{
    Self = 0,
    TargetEnemy = 1,
    RadiusEnemies = 2 //半径内敌人
}

public enum BuffType
{
    SetValue = 0,
    SetState = 1
    //...
}

public enum BuffSetValueType
{
    Attack = 0,
    HP = 1,
    MoveSpeed = 2
    //...
}

[System.Serializable]
public class Buff
{
    public BuffTarget target;
    public BuffType type;
    public BuffSetValueType setValueType;
    public int value;
}
