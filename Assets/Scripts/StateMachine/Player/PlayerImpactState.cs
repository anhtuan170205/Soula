using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int IMPACT_HASH = Animator.StringToHash("Impact");
    private const float CROSS_FADE_DURATION = 0.1f;
    private float m_duration = 0.5f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(IMPACT_HASH, CROSS_FADE_DURATION);
        Debug.Log("Player Impacted");
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        m_duration -= deltaTime;

        if (m_duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {

    }
}
