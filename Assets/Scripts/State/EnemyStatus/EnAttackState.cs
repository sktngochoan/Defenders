using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnAttackState : EnemyBaseState
{
    public EnAttackState(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        enemy.animator.SetTrigger("attack");
    }

    public override void UpdateState()
    {
        if (enemy.isHit == true)
        {
            enemy.ChangeState(new EnOnhitState(enemy));

        }
    }
}
