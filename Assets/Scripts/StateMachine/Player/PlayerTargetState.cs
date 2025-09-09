using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    private readonly int TargetBlendTreeHash = Animator.StringToHash("TargetBlendTree");
    private readonly int TargetForwardHash = Animator.StringToHash("TargetForward");
    private readonly int TargetRightHash = Animator.StringToHash("TargetRight");
    private const float AnimatorDampTime = 0.1f;

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
        UpdateAnimator(deltaTime);
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

    private void UpdateAnimatorValue(int parameterHash, float inputValue, float dampTime, float deltaTime)
    {
        float targetValue = 0;

        if (inputValue != 0)
        {
            targetValue = inputValue > 0 ? 1 : -1;
        }

        m_stateMachine.Animator.SetFloat(parameterHash, targetValue, dampTime, deltaTime);
    }

    public void UpdateAnimator(float deltaTime)
    {
        UpdateAnimatorValue(TargetForwardHash, m_stateMachine.InputReader.MovementValue.y, AnimatorDampTime, deltaTime);
        UpdateAnimatorValue(TargetRightHash,   m_stateMachine.InputReader.MovementValue.x, AnimatorDampTime, deltaTime);
    }

}
