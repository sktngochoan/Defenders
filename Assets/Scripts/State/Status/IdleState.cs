using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(PlayerController player) : base(player) { }
    public override void EnterState()
    {
        player.animator.SetFloat("Run", 0);
    }
    public override void UpdateState()
    {
        float move = player.joystick.Horizontal;

        if (Mathf.Abs(move) > 0)
        {
            player.ChangeState(new RunningState(player));
        }
        if(player.playerEntity.HP <= 0)
        {
            player.ChangeState(new DeadState(player));
        }
    }
}
