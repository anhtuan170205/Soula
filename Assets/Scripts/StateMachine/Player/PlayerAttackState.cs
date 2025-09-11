using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private Attack m_attack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    { 
        m_attack = m_stateMachine.Attacks[attackId];
    }
    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(m_attack.AnimationName, 0.1f);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
