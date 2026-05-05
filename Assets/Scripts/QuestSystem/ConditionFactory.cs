using UnityEngine;

public static class ConditionFactory
{
    public static ICondition Create(ConditionData data, StepRuntime step)
    {
        return data.type switch
        {
            "CollectItem" => new CollectItemCondition(data),
            "TalkToNPC" => new TalkCondition(data),
            _ => null
        };
    }
}
