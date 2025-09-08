using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine m_stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        m_stateMachine.Controller.Move((motion + m_stateMachine.ForceReceiver.Movement) * deltaTime);
    }
}
