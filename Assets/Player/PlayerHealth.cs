using System;
using UnityEngine;

public class PlayerHealth : Health
{
    public event Action<float, float> OnHealthChanged;
    private GameManager gameManager;


    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    protected override void HealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
    }

    override protected void Death()
    {
        isAlive = false;
        Debug.Log("Player " + name + " died.");
        gameManager.GameOver();
    }
}
