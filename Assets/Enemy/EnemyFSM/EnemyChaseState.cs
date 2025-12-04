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

        if (enemy.gravity != 0){
            enemy.GoInDirection(Vector3.ProjectOnPlane(enemy.GetVectorToLastPlayerPosition(), enemy.transform.up).normalized * enemy.chaseSpeed);
            if (Vector3.SignedAngle(enemy.transform.forward, toPlayer, -enemy.transform.right) > 45)
                enemy.JumpTo(enemy.UpdateLastPlayerPosition());
        }
        else
        {
            enemy.FlyInDirection(enemy.GetVectorToLastPlayerPosition().normalized * enemy.chaseSpeed + enemy.transform.up * 6);
            Debug.DrawRay(enemy.transform.position, enemy.GetVectorToLastPlayerPosition().normalized * enemy.chaseSpeed, Color.green);
        }
        
        //Debug.Log("Angle: " + Vector3.SignedAngle(enemy.transform.forward, toPlayer, -enemy.transform.right));
        
        enemy.enemyAttack.SetDirection(toPlayer.normalized);
        
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
