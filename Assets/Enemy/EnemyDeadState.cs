using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("IM A DEAD ENEMY");
        enemy.animator.enabled = false;
        enemy.SetRagdollColliders(true);
        enemy.SetRagdollRigidBody(true);
        enemy.enemyAttack.StopAttack();
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        enemy.GoInDirection(Vector3.zero); 
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
