using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalEnemy : PlayerEnemy
{
    public int slowSpeed;
    void Start()
    {
        slowSpeed = 2;
        hp = 35;
        currentHp = hp;
        speed = 1f;
        damage = 10;
        exp = 7;
        isSlow = false;
    }
}
