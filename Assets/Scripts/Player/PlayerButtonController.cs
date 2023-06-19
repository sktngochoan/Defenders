using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonController : MonoBehaviour
{
    public PlayerCombat playerCombat;
    void Start()
    {
        
    }
    public void normalAttack()
    {
        playerCombat.Attack();
    }
}
