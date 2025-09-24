using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private CharacterController m_controller;
    private Rigidbody[] m_allRigidbodies;
    private Collider[] m_allColliders;

    private void Start()
    {
        m_allColliders = GetComponentsInChildren<Collider>(true);
        m_allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        ToggleRadoll(false);
    }

    public void ToggleRadoll(bool isRagdoll)
    {
        foreach (Collider collider in m_allColliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }
        foreach (Rigidbody rigidbody in m_allRigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        m_controller.enabled = !isRagdoll;
        m_animator.enabled = !isRagdoll;
    }
}
