using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnOnhitState : EnemyBaseState
{
    public EnOnhitState(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        enemy.animator.SetTrigger("onhit");
    }

    public override void UpdateState()
    {
        if (enemy.isHit == false)
        {
            enemy.ChangeState(new EnRunningState(enemy));
            enemy.isHit = false;
        }
        if (enemy.hit == true)
        {
            enemy.ChangeState(new EnAttackState(enemy));
            enemy.isHit = false;
        }

    }

    public override void ExitState()
    {
        enemy.isHit = false; // Đặt lại giá trị isHit thành false khi kết thúc trạng thái EnOnhitState
        enemy.hit = false; // Đặt lại giá trị hit thành false khi kết thúc trạng thái EnOnhitState
    }
}
