using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private readonly int LOCOMOTION_HASH = Animator.StringToHash("Locomotion");
    private readonly int SPEED_HASH = Animator.StringToHash("Speed");
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.1f;
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(LOCOMOTION_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            m_stateMachine.SwitchState(new EnemyIdleState(m_stateMachine));
        }
        else if (IsInAttackRange())
        {
            m_stateMachine.SwitchState(new EnemyAttackState(m_stateMachine));
        }
        MoveToPlayer(deltaTime);
        FacePlayer();
        m_stateMachine.Animator.SetFloat(SPEED_HASH, 1, ANIMATOR_DAMP_TIME, deltaTime);
    }

    public override void Exit()
    {
        m_stateMachine.Agent.ResetPath();
        m_stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        m_stateMachine.Agent.destination = m_stateMachine.Player.transform.position;
        Move(m_stateMachine.Agent.desiredVelocity.normalized * m_stateMachine.MoveSpeed, deltaTime);
        m_stateMachine.Agent.velocity = m_stateMachine.Controller.velocity;
    }

    private bool IsInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(m_stateMachine.Player.transform.position, m_stateMachine.transform.position);
        return distanceToPlayer <= m_stateMachine.AttackRange;
    }
}
