using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_maxHealth = 100;
    private int m_health;
    private bool m_isVulnerable = true;
    public bool IsDead => m_health == 0;

    public event Action OnTakeDamage;
    public event Action OnDie;

    private void Start()
    {
        m_health = m_maxHealth;
    }

    public void SetVulnerable(bool isVulnerable)
    {
        m_isVulnerable = isVulnerable;
    }

    public void DealDamage(int damage)
    {
        if (m_health <= 0) { return; }
        if (!m_isVulnerable) { return; }

        m_health = Mathf.Max(m_health - damage, 0);
        Debug.Log($"Health: {m_health}/{m_maxHealth} of {gameObject.name}");
        OnTakeDamage?.Invoke();

        if (m_health == 0)
        {
            OnDie?.Invoke();
        }
    }
}
