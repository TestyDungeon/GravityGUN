using System.Collections;
using UnityEngine;

public class Grapple : Item
{
    private LineRenderer lr;
    [SerializeField] private float range;
    private float trajectoryHeight;
    [SerializeField] private float delay;
    [SerializeField] private float upForce;

    [SerializeField] private Transform grappleStart;
    private float g;

    private bool grappling;
    private bool swinging;
    private Vector3 grapplePoint;

    private RaycastHit hit;
    private MovementController mc = null;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = player.GetComponent<MovementController>().getGravity();
    }

    void Start()
    {
        mc = player.GetComponent<MovementController>();
    } 

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartGrapple();
            
        }
        if (swinging)
        {
            Swing();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            lr.enabled = false;
            if (grappling)
            {
                Invoke(nameof(LaunchGrapple), delay);
                grappling = false;
            }
            if (swinging)
                swinging = false;
        }
        
    }

    void LateUpdate()
    {
        if (grappling || swinging)
        {
            lr.SetPosition(0, grappleStart.position);
        }
    }

    private void Swing()
    {
        Vector3 vel = mc.getVelocity();
        Vector3 swingVelocity = Vector3.ProjectOnPlane(vel, transform.position - hit.point).normalized;
        mc.setVelocityDir(swingVelocity);
        //mc.addVelocity(swingVelocity);
    }


    private void StartGrapple()
    {
        Vector3 dir = cameraPivot.forward;
        if (Physics.Raycast(cameraPivot.position, dir, out hit, range, 1, QueryTriggerInteraction.Ignore))
        {
            grapplePoint = hit.point;

            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);
            grappling = true;
            Invoke(nameof(StopGrappling), 0.2f);

        }

    }

    private void LaunchGrapple()
    {
        //player.GetComponent<PlayerMovement>().append_vel(Vector3.up * 10);\
        CancelInvoke();
        player.GetComponent<PlayerMovement>().reset_vel();
        player.GetComponent<PlayerMovement>().append_vel(CalculateVelocity(cameraPivot.position, grapplePoint));
    }

    private void StopGrappling()
    {
        grappling = false;
        swinging = true;
    }
    
    private Vector3 CalculateVelocity(Vector3 start, Vector3 end)
    {
        Vector3 up = player.transform.up;

        // Bottom of the player (for reference)
        float playerHeight = player.GetComponent<CapsuleCollider>().height;
        Vector3 lowestPoint = player.transform.position - up * (playerHeight / 2f);

        // Vertical displacement from player bottom to grapple point
        float grappleY = Vector3.Dot(end - lowestPoint, up);

        // Determine the apex of the arc
        float apexHeight = grappleY + upForce; // desired arc above grapple point

        // Make sure apex is always above player's bottom
        apexHeight = Mathf.Max(apexHeight, 0.5f); // minimum 0.5 meters to avoid NaN

        trajectoryHeight = apexHeight;

        // Total displacement
        Vector3 displacement = end - start;

        // Vertical and horizontal components
        float displacementY = Vector3.Dot(displacement, up); // vertical
        Vector3 displacementXZ = Vector3.ProjectOnPlane(displacement, up); // horizontal

        // Gravity
         // positive number, magnitude of downward acceleration

        // Vertical velocity to reach apex
        float velocityY = Mathf.Sqrt(2f * g * apexHeight);

        // Time to reach apex
        float timeUp = velocityY / g;

        // Time to fall from apex to target
        float fallHeight = apexHeight - displacementY;
        float timeDown = Mathf.Sqrt(Mathf.Max(2f * fallHeight / g, 0.01f)); // avoid sqrt(0)

        float totalTime = timeUp + timeDown;

        // Horizontal velocity needed
        Vector3 velocityXZ = displacementXZ / totalTime;

        // Final velocity
        return 1.1f*(velocityXZ + up * velocityY);
    }

}
