using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : BaseState
{
    public HurtState(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        player.animator.SetTrigger("Ishit");
    }

    public override void UpdateState()
    {
        float move = Input.GetAxis("Horizontal");
        if (Mathf.Abs(move) != 0)
        {
            player.ChangeState(new RunningState(player));
        }
        else
        {
            player.ChangeState(new IdleState(player));
        }
    }

    public override void ExitState()
    {
        //player.isAttacking = false;
    }
}
