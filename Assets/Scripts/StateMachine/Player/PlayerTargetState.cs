using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    private readonly int TargetBlendTreeHash = Animator.StringToHash("TargetBlendTree");
    public PlayerTargetState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.InputReader.CancelEvent += OnCancel;
        m_stateMachine.Animator.Play(TargetBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log(m_stateMachine.Targeter.CurrentTarget.name);
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        m_stateMachine.Targeter.Cancel();
        m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
    }
}
