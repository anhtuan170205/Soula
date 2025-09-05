using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.InputReader.TargetEvent += OnTarget;
    }


    public override void Tick(float deltaTime)
    {
        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            m_stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        m_stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        Vector3 movement = CalculateMovement();

        m_stateMachine.Controller.Move(movement * m_stateMachine.FreeLookMoveSpeed * deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        m_stateMachine.SwitchState(new PlayerTargetState(m_stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = m_stateMachine.MainCameraTransform.forward;
        Vector3 right = m_stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        return forward * m_stateMachine.InputReader.MovementValue.y + right * m_stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        m_stateMachine.transform.rotation = Quaternion.Lerp(
            m_stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * m_stateMachine.RotationDamping);
    }
}
