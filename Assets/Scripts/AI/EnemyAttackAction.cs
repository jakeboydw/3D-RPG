using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyAttack", story: "Try to [Attack] [Player]", category: "Action", id: "601f9eb5231bf474afc869152ccefbf6")]
public partial class EnemyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyCombat> Attack;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        var player = Player.Value;
        var attack = Attack.Value;
        attack.Attack(player);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

