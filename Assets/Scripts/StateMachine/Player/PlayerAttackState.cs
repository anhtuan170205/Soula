using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float m_previousFrameTime;
    private bool m_hasAppliedForce;
    private Attack m_attack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        m_attack = m_stateMachine.Attacks[attackIndex];
    }
    public override void Enter()
    {
        m_stateMachine.Animator.CrossFadeInFixedTime(m_attack.AnimationName, m_attack.TransitionDuration);
        m_stateMachine.WeaponDamage.SetAttackDamage(m_attack.Damage);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= m_previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= m_attack.ForceTime)
            {
                TryApplyForce();
            }
            if (m_stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (m_stateMachine.Targeter.CurrentTarget != null)
            {
                m_stateMachine.SwitchState(new PlayerTargetState(m_stateMachine));
            }
            else
            {
                m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
            }
        }

        m_previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = m_stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = m_stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (m_stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!m_stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        return 0f;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (m_attack.ComboStateIndex == -1) { return; }
        if (normalizedTime < m_attack.ComboAttackTime) { return; }

        m_stateMachine.SwitchState(new PlayerAttackState(m_stateMachine, m_attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (m_hasAppliedForce) { return; }
        m_hasAppliedForce = true;
        m_stateMachine.ForceReceiver.AddForce(m_stateMachine.transform.forward * m_attack.Force);
    }
}
