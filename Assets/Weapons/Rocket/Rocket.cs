using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //private Camera player_camera;
    [SerializeField] private float rocketSpeed = 1f;
    [SerializeField] private float explosionRadius = 10;
    [SerializeField] private float explosionForce = 5;
    [SerializeField] private GameObject explosionPrefab;
    private Vector3 pos; 
    private Vector3 pre_pos;
    [HideInInspector] public Transform rocketStart;
    [HideInInspector] public int damage;

    int layer_mask;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //player_camera = GameObject.Find("Camera").GetComponent<Camera>();
        pre_pos = transform.position;
        layer_mask = 1 << 3;
        layer_mask = ~layer_mask;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("OBJECT: ");
        if ((rocketStart.position - transform.position).magnitude > 1)
            GetComponent<MeshRenderer>().enabled = true;

        transform.Translate(transform.forward * rocketSpeed, Space.World);
        if(Physics.Raycast(pre_pos, transform.forward, Vector3.Distance(pre_pos, transform.position), layer_mask, QueryTriggerInteraction.Ignore)){
            var surrounding_objects = Physics.OverlapSphere(transform.position, explosionRadius, ~0, QueryTriggerInteraction.Ignore);
            GameObject particles = Instantiate(explosionPrefab, transform.position-transform.forward*0.5f, Quaternion.identity);
            Destroy(particles, 2f);
            foreach (var obj in surrounding_objects)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                PlayerMovement player = obj.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    //player.set_vel(player.get_vel() + (0.2f * (obj.transform.position - transform.position).normalized * explosion_radius - (obj.transform.position - transform.position)));
                    player.append_vel(explosionForce * (obj.transform.position - transform.position).normalized);
                }
                if (obj.GetComponent<Health>() != null)
                {
                    Damage(obj.gameObject);
                }
                if (rb != null)
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 3, ForceMode.Impulse);

            }
            
            
            Debug.DrawRay(pre_pos, transform.forward, Color.white);
            Destroy(gameObject);
        }
    }

    private void Damage(GameObject victim)
    {
        if (victim.GetComponent<Health>() != null)
        {
            victim.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void LateUpdate()
    {
        pre_pos = transform.position;
    }
}
