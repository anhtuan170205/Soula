using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FREE_LOOK_BLEND_TREE_HASH = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FREE_LOOK_SPEED_HASH = Animator.StringToHash("FreeLookSpeed");
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.1f;
    private bool m_shouldFade;
    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    { 
        m_shouldFade = shouldFade;
    }

    public override void Enter()
    {
        m_stateMachine.InputReader.TargetEvent += OnTarget;
        m_stateMachine.InputReader.DodgeEvent += OnDodge;
        m_stateMachine.InputReader.JumpEvent += OnJump;

        m_stateMachine.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 0f);
        if (m_shouldFade)
        {
            m_stateMachine.Animator.CrossFadeInFixedTime(FREE_LOOK_BLEND_TREE_HASH, CROSS_FADE_DURATION);
        }
        else
        {
            m_stateMachine.Animator.Play(FREE_LOOK_BLEND_TREE_HASH);
        }
    }

    public override void Tick(float deltaTime)
    {
        if (m_stateMachine.InputReader.IsAttacking)
        {
            m_stateMachine.SwitchState(new PlayerAttackState(m_stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * m_stateMachine.FreeLookMoveSpeed, deltaTime);

        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            m_stateMachine.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 0, ANIMATOR_DAMP_TIME, deltaTime);
            return;
        }

        m_stateMachine.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 1, ANIMATOR_DAMP_TIME, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.TargetEvent -= OnTarget;
        m_stateMachine.InputReader.DodgeEvent -= OnDodge;
        m_stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnTarget()
    {
        if (!m_stateMachine.Targeter.SelectTarget()) { return; }
        m_stateMachine.SwitchState(new PlayerTargetState(m_stateMachine));
    }

    private void OnDodge()
    {
        m_stateMachine.SwitchState(new PlayerDodgeState(m_stateMachine));
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
    
    private void OnJump()
    {
        m_stateMachine.SwitchState(new PlayerJumpState(m_stateMachine));
    }
}
