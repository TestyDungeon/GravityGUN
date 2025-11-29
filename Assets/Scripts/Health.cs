using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDeath;
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;
    protected bool isAlive = true;
    

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("DAMAGE: " + damageAmount);
        if (currentHealth <= 0)
            return;
        currentHealth -= damageAmount;
        HealthChanged();
        Debug.Log(name + " health: " + currentHealth);

        if (currentHealth <= 0 && isAlive)
            Death();
    }

    public void Heal(int healAmount)
    {
        currentHealth += Mathf.Clamp(healAmount, 0, maxHealth - currentHealth);
        HealthChanged();
    }

    virtual protected void HealthChanged()
    {
        
    }

    virtual protected void Death()
    {
        OnDeath?.Invoke();
        Debug.Log(name + " died.");
        //Destroy(gameObject);
    }
}
