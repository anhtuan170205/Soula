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
        Vector3 movement = new Vector3();
        movement.x = m_stateMachine.InputReader.MovementValue.x;
        movement.z = m_stateMachine.InputReader.MovementValue.y;
        movement.y = 0;

        m_stateMachine.Controller.Move(movement * m_stateMachine.FreeLookMoveSpeed * deltaTime);
        m_stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void Exit()
    {
    }
}
