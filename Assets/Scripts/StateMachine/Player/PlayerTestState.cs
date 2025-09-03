using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            m_stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
            return;
        }

        m_stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
        Vector3 movement = CalculateMovement();
        
        m_stateMachine.Controller.Move(movement * m_stateMachine.FreeLookMoveSpeed * deltaTime);
        m_stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void Exit()
    {
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
}
