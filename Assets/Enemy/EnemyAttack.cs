using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private Vector3 direction;
    GameObject newProjectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Attack()
    {
        StartCoroutine(LaunchProjectile());
    }

    public void StopAttack()
    {
        StopAllCoroutines();
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    IEnumerator LaunchProjectile()
    {
        while (true)
        {
            newProjectile = Instantiate(projectile, transform.position, Quaternion.LookRotation(direction));
            newProjectile.GetComponent<EnemyProjectile>().projectileStart = transform;
            yield return new WaitForSeconds(Random.Range(0.5f, 2));
        }
    }
}
