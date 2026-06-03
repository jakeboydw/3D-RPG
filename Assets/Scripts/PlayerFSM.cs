using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    Locomotion,
    Jump,
    Attack
}

public class PlayerFSM : MonoBehaviour
{
    public Player Player { get; private set; }

    private Dictionary<PlayerStateType, PlayerState> states = new();
    private PlayerState currentState;

    private void Start()
    {
        Player = GetComponent<Player>();

        states.Add(PlayerStateType.Locomotion, new LocomotionState(this));
        states.Add(PlayerStateType.Jump, new JumpState(this));
        states.Add(PlayerStateType.Attack, new AttackState(this));

        ChangeState(PlayerStateType.Locomotion);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }

    public void ChangeState(PlayerStateType state)
    {
        if (!states.ContainsKey(state)) return;
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[state];
        currentState.OnEnter();
    }
}
