using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    public float BaseValue;

    public List<StatModifier> modifiers = new List<StatModifier>();

    public float Value
    {
        get
        {
            float finalValue = BaseValue;
            float percent = 0f;

            foreach (StatModifier modifier in modifiers)
            {
                switch (modifier.modType)
                {
                    case StatModType.Flat:
                        finalValue += modifier.value;
                        break;
                    case StatModType.Percent:
                        percent += modifier.value;
                        break;
                }
            }

            finalValue *= 1 + percent;

            return finalValue;
        }
    }

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(object source)
    {
        modifiers.RemoveAll(m => m.source == source);
    }
}
