using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private float m_timer;

    public override void Enter()
    {
        m_stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        m_timer += deltaTime;
        Debug.Log(m_timer);
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        m_stateMachine.SwitchState(new PlayerTestState(m_stateMachine));
    }
}
