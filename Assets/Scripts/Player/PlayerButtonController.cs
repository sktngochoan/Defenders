using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonController : MonoBehaviour
{
    public Player player;
    void Start()
    {
        
    }
    public void normalAttack()
    {
        player.Attack();
    }
}
