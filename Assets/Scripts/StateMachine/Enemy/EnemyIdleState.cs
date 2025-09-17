using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
    private readonly int SPEED_HASH = Animator.StringToHash("Speed");
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            m_stateMachine.SwitchState(new EnemyChaseState(m_stateMachine));
        }
        FacePlayer();
        m_stateMachine.Animator.SetFloat(SPEED_HASH, 0, ANIMATOR_DAMP_TIME, deltaTime);
    }

    public override void Exit()
    {

    }
}
