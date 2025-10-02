using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    private readonly int CLIMB_HASH = Animator.StringToHash("Climb");
    private const float CROSS_FADE_DURATION = 0.1f;
    private readonly Vector3 CLIMB_OFFSET = new Vector3(0f, 2.35f, 0.65f);

    public PlayerClimbState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(CLIMB_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(m_stateMachine.Animator, "Climb") < 1f) { return; }
        m_stateMachine.Controller.enabled = false;
        m_stateMachine.transform.Translate(CLIMB_OFFSET, Space.Self);
        m_stateMachine.Controller.enabled = true;
        m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine, false));
    }

    public override void Exit()
    {
        m_stateMachine.Controller.Move(Vector3.zero);
        m_stateMachine.ForceReceiver.Reset();
    }
}
