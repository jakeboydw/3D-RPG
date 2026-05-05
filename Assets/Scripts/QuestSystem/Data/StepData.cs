using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepData
{
    public string stepID;
    public string description;

    public string dialogueID;

    public List<ConditionData> conditions;
    public List<EffectData> onStartEffects;
    public List<EffectData> onFinishEffects;
}
