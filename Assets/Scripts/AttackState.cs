using UnityEngine;

public class AttackState : PlayerState
{
    public AttackState(PlayerFSM fsm) : base(fsm)
    {
    }

    public override void OnEnter()
    {
        AttackData attack = player.Combat.CurrentAttack;
        if (attack == null) return;

        player.Animator.CrossFade(attack.attackAnimation.name, 0.1f);
    }
}
