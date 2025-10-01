using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private NavMeshAgent m_agent;
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

        if (m_agent != null)
        {
            if (m_impact == Vector3.zero)
            {
                m_agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        m_impact += force;
        if (m_agent != null)
        {
            m_agent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        m_verticalVelocity += jumpForce;
    }

    public void Reset()
    {
        m_impact = Vector3.zero;
        m_verticalVelocity = 0f;
    }
}
