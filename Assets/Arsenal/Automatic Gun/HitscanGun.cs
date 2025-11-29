using UnityEngine;
using System.Collections;

public class HitscanGun : Gun
{
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject muzzleFlashPrefab;
    
    [SerializeField] private float range = 300;
    [SerializeField] private float force = 20;
    [SerializeField] private float knockbackForce = 0.1f;
    [SerializeField] private float spread = 20;
    
    
    private int layerMask = ~(1 << 3);


    protected override void Shoot()
    {
        
        //animator?.SetTrigger("Shoot");
        animator.speed = 2;
        animator.Play("Gun_Shoot");

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = cameraPivot.forward + new Vector3(x, y, 0);
        float distance = range;
        Vector3 target = transform.position + direction * distance;

        Knockback(-direction);

        if (Physics.Raycast(cameraPivot.position, direction, out RaycastHit hit, range, layerMask, QueryTriggerInteraction.Ignore))
        {
            target = hit.point;
            distance = hit.distance;
            if (hit.rigidbody)
            {
                Debug.Log("HIT " + hit.transform.position);
                hit.rigidbody.AddForceAtPosition(transform.forward * force, hit.point, ForceMode.Impulse);
            }
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
        SpawnMuzzleFlash();
        StartCoroutine(SpawnBulletTrail(target));
    }

    

    private void Knockback(Vector3 dir)
    {
        player.GetComponent<PlayerMovement>().append_vel(dir * knockbackForce);
    }

    private void SpawnMuzzleFlash()
    {
        GameObject particles = Instantiate(muzzleFlashPrefab, gunTip.position, transform.rotation);
        Destroy(particles, 0.5f);
    }

    IEnumerator SpawnBulletTrail(Vector3 target)
    {
        float time = 0;
        TrailRenderer trail = Instantiate(bulletTrail, gunTip.position, Quaternion.identity);
        while (time < 1)
        {
            trail.transform.position += (target - trail.transform.position).normalized * 200 * Time.deltaTime;
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = target;

        Destroy(trail.gameObject, trail.time);
    }
}
