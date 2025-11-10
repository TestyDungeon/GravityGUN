using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("IM A CHASING ENEMY");
        Vector3 toPlayer = enemy.GetVectorToLastPlayerPosition();
        enemy.enemyAttack.SetDirection(toPlayer.normalized);
        enemy.enemyAttack.Attack();
        enemy.animator.speed = 2;
        enemy.animator.Play("Walk");
        
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        enemy.UpdateLastPlayerPosition();
        Vector3 toPlayer = enemy.GetVectorToLastPlayerPosition();
        

        enemy.GoInDirection(Vector3.ProjectOnPlane(enemy.GetVectorToLastPlayerPosition(), enemy.transform.up).normalized * enemy.chaseSpeed);
        enemy.enemyAttack.SetDirection(toPlayer.normalized);
        
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
