using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine m_stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.m_stateMachine = stateMachine;
    }

}
