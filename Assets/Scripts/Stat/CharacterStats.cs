using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    private Dictionary<StatType, CharacterStat> stats = new();

    public CharacterStats()
    {
        stats[StatType.MaxHP] = new CharacterStat();
        stats[StatType.Attack] = new CharacterStat();
        stats[StatType.MoveSpeed] = new CharacterStat();
    }

    public CharacterStat GetStat(StatType type)
    {
        if (stats.TryGetValue(type, out CharacterStat stat))
        {
            return stat;
        }
        return null;
    }
}
