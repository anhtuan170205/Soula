using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JUMP_HASH = Animator.StringToHash("Jump");
    private const float CROSS_FADE_DURATION = 0.1f;
    private Vector3 m_momentum;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.ForceReceiver.Jump(m_stateMachine.JumpForce);
        m_stateMachine.Animator.CrossFadeInFixedTime(JUMP_HASH, CROSS_FADE_DURATION);
        m_momentum = m_stateMachine.Controller.velocity;
        m_momentum.y = 0f;
        m_stateMachine.LedgeDetector.OnLedgeDetected += HandleLedgeDetect;
    }

    public override void Tick(float deltaTime)
    {
        Move(m_momentum, deltaTime);

        if (m_stateMachine.Controller.velocity.y <= 0f)
        {
            m_stateMachine.SwitchState(new PlayerFallState(m_stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        m_stateMachine.LedgeDetector.OnLedgeDetected -= HandleLedgeDetect;
    }

    private void HandleLedgeDetect(Vector3 ledgeForward, Vector3 closestPoint)
    {
        m_stateMachine.SwitchState(new PlayerHangState(m_stateMachine, ledgeForward, closestPoint));
    }
}
