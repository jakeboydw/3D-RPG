using UnityEngine;

public abstract class PlayerState
{
    protected PlayerFSM fsm;
    protected Player player;

    public PlayerState(PlayerFSM fsm)
    {
        this.fsm = fsm;
        player = fsm.Player;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() { }

    public virtual void OnExit() { }
}
