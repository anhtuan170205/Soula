using UnityEngine;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider m_playerCollider;
    private List<Collider> m_collidedColliders = new List<Collider>();

    private void OnEnable()
    {
        m_collidedColliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == m_playerCollider) { return; }
        if (m_collidedColliders.Contains(other)) { return; }

        m_collidedColliders.Add(other);
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(10);
        }
    }
}
