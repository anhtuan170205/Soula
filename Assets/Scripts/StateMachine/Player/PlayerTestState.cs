using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement.x = m_stateMachine.InputReader.MovementValue.x;
        movement.z = m_stateMachine.InputReader.MovementValue.y;
        movement.y = 0;
        m_stateMachine.transform.Translate(movement * deltaTime);
    }

    public override void Exit()
    {
    }
}
