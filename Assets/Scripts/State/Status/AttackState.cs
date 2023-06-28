using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        player.animator.SetTrigger("Attack");
    }

    public override void UpdateState()
    {
        float move = Input.GetAxis("Horizontal");
        if (Mathf.Abs(move) == 0)
        {
            player.ChangeState(new IdleState(player));
        }
    }

    public override void ExitState()
    {
        //player.isAttacking = false;
    }

}
