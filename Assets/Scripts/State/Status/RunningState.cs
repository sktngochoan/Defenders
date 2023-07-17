using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public RunningState(PlayerController player) : base(player) { }
    public override void EnterState()
    {
        player.animator.SetFloat("Run", Mathf.Abs(player.joystick.Horizontal));
    }

    public override void UpdateState()
    {
        float move = player.joystick.Horizontal;

        if (Mathf.Abs(move) == 0)
        {
            player.ChangeState(new IdleState(player));
        }
        if(player.playerEntity.CurrentHp <= 0)
        {
            player.ChangeState(new DeadState(player));
        }
        if (player.isHit == true)
        {
            player.ChangeState(new HurtState(player));
        }
    }
}
