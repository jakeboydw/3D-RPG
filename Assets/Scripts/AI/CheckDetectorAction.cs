using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckDetector", story: "Check if [Detector] has a target and set [TargetDetected]", category: "Action", id: "379cb9e331c699812238a21464eb04a5")]
public partial class CheckDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Detector> Detector;
    [SerializeReference] public BlackboardVariable<bool> TargetDetected;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Detector.Value == null)
        {
            TargetDetected.Value = false;
            return Status.Failure;
        }

        TargetDetected.Value = Detector.Value.HasTarget();

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

