using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

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
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int healAmount)
    {
        currentHealth += Mathf.Clamp(healAmount, 0, maxHealth - currentHealth);
    }

    private void Die()
    {
        Debug.Log(name + " died.");
        Destroy(gameObject);
    }
}
