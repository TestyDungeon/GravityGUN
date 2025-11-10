
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyFightState FightState = new EnemyFightState();
    [HideInInspector] public MovementController movementController;

    public float chaseSpeed = 7;
    public float idleSpeed = 7;
    [HideInInspector] public Vector3 idleDir = Vector3.zero;
    public float rotationSpeed = 7;

    private Transform player;
    private Vector3 lastPlayerPosition = Vector3.zero;

    private CapsuleCollider capsuleCollider;
    [HideInInspector] public float sight;
    [HideInInspector] public EnemyAttack enemyAttack;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 randomPosition;
    [HideInInspector] public Vector3 enemyVelocity = Vector3.zero;
    [HideInInspector] public Vector3 enemyExternalVelocity = Vector3.zero;

    private AINavigation aiNavigation;

    private NavMeshPath path;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        aiNavigation = new AINavigation();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttack = GetComponent<EnemyAttack>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        sight = GetComponentInChildren<SphereCollider>().radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        movementController = GetComponent<MovementController>();
        currentState = IdleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);

    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public Vector3 GetVectorToLastPlayerPosition()
    {
        return lastPlayerPosition - transform.position;

    }

    public bool IsPlayerInSight()
    {
        if ((player.position - transform.position).sqrMagnitude < sight * sight)
        {
            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hit, 100, ~((1 << 8) | (1 << 6))))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    lastPlayerPosition = hit.transform.position;
                    Debug.DrawRay(lastPlayerPosition, transform.up, Color.white, 1f);
                    return true;
                }
            }
        }
        return false;
    }

    public void UpdateLastPlayerPosition()
    {
        if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hit, 100, ~((1 << 8) | (1 << 6) | (1 << 9))))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    lastPlayerPosition = hit.transform.position;
                    Debug.DrawRay(lastPlayerPosition, transform.up, Color.white, 1f);
                }
            }
    }

    public void InvokeRandomDirection()
    {
        InvokeRepeating("GetRandomDirection", 0, 2);
    }

    public void GoInDirection(Vector3 dir)
    {
        enemyVelocity = Vector3.Project(enemyVelocity, transform.up) + Vector3.ProjectOnPlane(dir, transform.up);
            
        Quaternion targetRotation = Quaternion.LookRotation(dir, transform.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        enemyVelocity = movementController.Move(enemyVelocity);
        Debug.DrawRay(transform.position, enemyVelocity);
    }


    public void GetRandomDirection()
    {
        idleDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Debug.Log("Random " + idleDir);
        idleDir = Vector3.ProjectOnPlane(idleDir, transform.up).normalized;
    }
    
}
