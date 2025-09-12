using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private float m_drag = 0.3f;
    private Vector3 m_dampingVelocity;
    private Vector3 m_impact;
    private float m_verticalVelocity;

    public Vector3 Movement => m_impact + Vector3.up * m_verticalVelocity;

    private void Update()
    {
        if (m_controller.isGrounded && m_verticalVelocity < 0)
        {
            m_verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            m_verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        m_impact = Vector3.SmoothDamp(m_impact, Vector3.zero, ref m_dampingVelocity, m_drag);
    }

    public void AddForce(Vector3 force)
    {
        m_impact += force;
    }
}
