using UnityEngine;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider m_bearerCollider;
    private List<Collider> m_collidedColliders = new List<Collider>();
    private int m_attackDamage = 10;

    private void OnEnable()
    {
        m_collidedColliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == m_bearerCollider) { return; }
        if (m_collidedColliders.Contains(other)) { return; }

        m_collidedColliders.Add(other);
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(m_attackDamage);
        }
    }

    public void SetAttackDamage(int damage)
    {
        m_attackDamage = damage;
    }
}
