using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int IMPACT_HASH = Animator.StringToHash("Impact");
    private const float CROSS_FADE_DURATION = 0.1f;
    private float m_duration = 1f;
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(IMPACT_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        m_duration -= deltaTime;
        if (m_duration <= 0f)
        {
            m_stateMachine.SwitchState(new EnemyIdleState(m_stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
