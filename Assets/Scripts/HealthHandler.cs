using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public float currentHealth = 100f;
    public float currentNormalizedHealth { get => currentHealth / maxHealth; }
    public float maxHealth = 100f;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }
}
