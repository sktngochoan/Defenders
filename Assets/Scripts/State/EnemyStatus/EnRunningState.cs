using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnRunningState : EnemyBaseState
{
    public EnRunningState(EnemyController enemy) : base(enemy) { }
    public override void EnterState()
    {
        enemy.animator.SetFloat("run", 1);
        //enemy.animator.SetTrigger("run");
    }

    public override void UpdateState()
    {

        if (enemy.isHit)
        {

            enemy.ChangeState(new EnOnhitState(enemy));
            enemy.hit = true;
        }
        if (enemy.hit == true)
        {
            //enemy.isHit = true;
            enemy.ChangeState(new EnAttackState(enemy));

        }
        if (enemy.player != null && Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.distanceThreshold)
        {
            enemy.ChangeState(new EnAttackState(enemy));

        }
        else
        {
            MoveTowardsPlayer();
        }

    }
    private void MoveTowardsPlayer()
    {
        Vector3 direction = enemy.player.transform.position - enemy.transform.position;
        float distance = direction.magnitude;

        if (distance <= enemy.distanceThreshold)
        {
            enemy.rb.velocity = direction.normalized * enemy.force;
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
        else
        {
            enemy.rb.velocity = enemy.initialDirection * enemy.force;
            float rotationAngle = Mathf.Atan2(enemy.initialDirection.y, enemy.initialDirection.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }
}
