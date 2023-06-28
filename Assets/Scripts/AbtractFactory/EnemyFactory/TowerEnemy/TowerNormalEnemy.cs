using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNormalEnemy : TowerEnemy
{
    void Start()
    {
        hp = 50;
        speed = 1f;
        damage = 20;
        exp = 4;
        isSlow = false;
        if (isBoss == true)
        {
            base.InitializeBossStats();
        }
        currentHp = hp;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
}
