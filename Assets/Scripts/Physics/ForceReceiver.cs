using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController m_controller;
    private float m_verticalVelocity;

    public Vector3 Movement => Vector3.up * m_verticalVelocity;

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
    }

}
