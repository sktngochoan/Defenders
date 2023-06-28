using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        player.animator.SetTrigger("Dead");
    }
}
