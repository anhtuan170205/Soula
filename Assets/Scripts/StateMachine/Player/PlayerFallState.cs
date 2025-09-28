using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FALL_HASH = Animator.StringToHash("Fall");
    private const float CROSS_FADE_DURATION = 0.1f;
    private Vector3 m_momentum;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(FALL_HASH, CROSS_FADE_DURATION);
        m_momentum = m_stateMachine.Controller.velocity;
        m_momentum.y = 0f;
    }

    public override void Tick(float deltaTime)
    {
        Move(m_momentum, deltaTime);
        if (m_stateMachine.Controller.isGrounded)
        {
            ReturnToLocomotion();
        }
        FaceTarget();
    }

    public override void Exit()
    {

    }
}
