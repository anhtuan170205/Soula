using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private const float CROSS_FADE_DURATION = 0.1f;
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.WeaponDamage.SetAttackDamage(m_stateMachine.AttackDamage, m_stateMachine.Knockback);
        m_stateMachine.Animator.CrossFadeInFixedTime(ATTACK_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(m_stateMachine.Animator) >= 1f)
        {
            m_stateMachine.SwitchState(new EnemyChaseState(m_stateMachine));
        }
        FacePlayer();
    }

    public override void Exit()
    {

    }
}
