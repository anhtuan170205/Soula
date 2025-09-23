using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_maxHealth = 100;
    private int m_health;

    public event Action OnTakeDamage;

    private void Start()
    {
        m_health = m_maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (m_health <= 0) { return; }
        m_health = Mathf.Max(m_health - damage, 0);
        OnTakeDamage?.Invoke();
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {m_health}");
    }
}
