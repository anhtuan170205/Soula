using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DODGE_HASH = Animator.StringToHash("Dodge");
    private readonly int DODGE_BACK_HASH = Animator.StringToHash("DodgeBack");
    private const float CROSS_FADE_DURATION = 0.1f;
    private const float DODGE_DURATION = 0.8f;
    private const float DODGE_SPEED = 6f;

    private float m_dodgeTime;
    private Vector3 m_dodgeDirection;

    public PlayerDodgeState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Health.SetVulnerable(false);
        m_dodgeTime = DODGE_DURATION;

        if (m_stateMachine.Targeter.CurrentTarget != null)
        {
            Vector3 right   = m_stateMachine.transform.right   * m_stateMachine.InputReader.MovementValue.x;
            Vector3 forward = m_stateMachine.transform.forward * m_stateMachine.InputReader.MovementValue.y;
            m_dodgeDirection = (right + forward).normalized;

            if (m_dodgeDirection == Vector3.zero)
            {
                m_dodgeDirection = m_stateMachine.transform.forward;
            }
        }
        else
        {
            m_dodgeDirection = m_stateMachine.transform.forward;
        }

        bool isBackward = m_stateMachine.Targeter.CurrentTarget != null && Vector3.Dot(m_dodgeDirection, m_stateMachine.transform.forward) < 0f;

        int animHash = isBackward ? DODGE_BACK_HASH : DODGE_HASH;
        m_stateMachine.Animator.CrossFadeInFixedTime(animHash, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        m_dodgeTime -= deltaTime;

        if (m_dodgeTime <= 0f)
        {
            if (m_stateMachine.Targeter.CurrentTarget != null)
            {
                m_stateMachine.SwitchState(new PlayerTargetState(m_stateMachine));
            }
            else
            {
                m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
            }
        }

        Move(m_dodgeDirection * DODGE_SPEED, deltaTime);
    }

    public override void Exit()
    {
        m_stateMachine.Health.SetVulnerable(true);
    }
}
