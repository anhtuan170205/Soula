using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private readonly int BLOCK_HASH = Animator.StringToHash("Block");
    private const float CROSS_FADE_DURATION = 0.1f;
    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Health.SetVulnerable(false);
        m_stateMachine.Animator.CrossFadeInFixedTime(BLOCK_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (!m_stateMachine.InputReader.IsBlocking)
        {
            m_stateMachine.SwitchState(new PlayerTargetState(m_stateMachine));
            return;
        }
        if (m_stateMachine.Targeter.CurrentTarget == null)
        {
            m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        m_stateMachine.Health.SetVulnerable(true);
    }
}
