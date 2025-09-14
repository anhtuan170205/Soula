using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine m_stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        float distanceToPlayer = Vector3.Distance(m_stateMachine.Player.transform.position, m_stateMachine.transform.position);
        return distanceToPlayer <= m_stateMachine.ChaseRange;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        m_stateMachine.Controller.Move((motion + m_stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

}
