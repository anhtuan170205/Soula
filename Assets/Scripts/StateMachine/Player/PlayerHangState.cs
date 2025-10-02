using UnityEngine;

public class PlayerHangState : PlayerBaseState
{
    private readonly int HANGING_HASH = Animator.StringToHash("Hanging");
    private const float CROSS_FADE_DURATION = 0.1f;
    private Vector3 m_ledgeForward;
    private Vector3 m_closestPoint;

    public PlayerHangState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
    {
        this.m_ledgeForward = ledgeForward;
        this.m_closestPoint = closestPoint;
    }

    public override void Enter()
    {
        m_stateMachine.transform.rotation = Quaternion.LookRotation(m_ledgeForward, Vector3.up);

        m_stateMachine.Controller.enabled = false;
        m_stateMachine.transform.position = m_closestPoint - (m_stateMachine.LedgeDetector.transform.position - m_stateMachine.transform.position);
        m_stateMachine.Controller.enabled = true;

        m_stateMachine.Animator.CrossFadeInFixedTime(HANGING_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        if (m_stateMachine.InputReader.MovementValue.y < 0f)
        {
            m_stateMachine.Controller.Move(Vector3.zero);
            m_stateMachine.ForceReceiver.Reset();
            m_stateMachine.SwitchState(new PlayerFallState(m_stateMachine));
        }
        else if (m_stateMachine.InputReader.MovementValue.y > 0f)
        {
            m_stateMachine.SwitchState(new PlayerClimbState(m_stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
