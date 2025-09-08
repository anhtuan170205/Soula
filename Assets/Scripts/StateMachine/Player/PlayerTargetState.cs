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
        if (m_stateMachine.Targeter.CurrentTarget == null)
        {
            m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();

        Move(movement * m_stateMachine.TargetMoveSpeed, deltaTime);
        FaceTarget();
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

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();
        movement += m_stateMachine.transform.right * m_stateMachine.InputReader.MovementValue.x;
        movement += m_stateMachine.transform.forward * m_stateMachine.InputReader.MovementValue.y;

        return movement;
    }
}
