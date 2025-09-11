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

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        if (m_stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 lookDirection = m_stateMachine.Targeter.CurrentTarget.transform.position - m_stateMachine.transform.position;
        lookDirection.y = 0;

        m_stateMachine.transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
