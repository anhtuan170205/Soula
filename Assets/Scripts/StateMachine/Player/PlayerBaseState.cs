using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine m_stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }
}
