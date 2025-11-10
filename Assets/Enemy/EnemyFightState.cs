using System.Collections;
using UnityEngine;

public class EnemyFightState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        enemy.movementController.Move(enemy.enemyVelocity);
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {

    }

    
}
