using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private const float CROSS_FADE_DURATION = 0.1f;
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(ATTACK_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
