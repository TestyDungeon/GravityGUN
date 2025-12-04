using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyHitResponder : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject bloodParticlesPrefab;
    private Health enemyHealth;


    void Awake()
    {
        enemyHealth = GetComponent<Health>();
            
    }

    public void TakeDamage(int damageAmount, Vector3 damagePoint, Vector3 normal)
    {
        GameObject particles = Instantiate(bloodParticlesPrefab, damagePoint, Quaternion.LookRotation(normal, transform.up));
        Destroy(particles, 2f);
        enemyHealth.TakeDamage(damageAmount);
        
    }
}
