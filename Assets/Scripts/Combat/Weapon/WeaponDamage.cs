using UnityEngine;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider m_bearerCollider;
    private List<Collider> m_collidedColliders = new List<Collider>();
    private int m_attackDamage;
    private float m_knockback;

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
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - m_bearerCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * m_knockback);
        }
    }

    public void SetAttackDamage(int damage, float knockback)
    {
        m_attackDamage = damage;
        m_knockback = knockback;
    }
}
