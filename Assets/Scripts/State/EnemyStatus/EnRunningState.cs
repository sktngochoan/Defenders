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
            enemy.isHit = false;
        }
        if (enemy.hit == true)
        {
            enemy.ChangeState(new EnAttackState(enemy));
            enemy.hit = false;
        }
    }
}
