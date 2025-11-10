using System.Collections;
using System.Dynamic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    Vector3 dir;
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.InvokeRepeating("GetRandomDirection", 0, 2);
        Debug.Log("IM AN IDLE ENEMY");
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {



        enemy.GoInDirection(enemy.idleDir * enemy.idleSpeed);

        if (enemy.IsPlayerInSight())
        {
            enemy.SwitchState(enemy.ChaseState);
        }
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {

    }

    
}
