using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonController : MonoBehaviour
{
    public PlayerCombat playerCombat;

    public void normalAttack()
    {
        playerCombat.NormalAttack();
    }

    public void hasagiSkill()
    {
        playerCombat.HasagiSkill();
    }

    public void dashSkill()
    {
        playerCombat.DashSkill();
    }
}
