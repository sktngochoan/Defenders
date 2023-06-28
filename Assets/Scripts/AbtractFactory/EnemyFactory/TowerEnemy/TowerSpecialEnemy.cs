using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpecialEnemy : TowerEnemy
{
    void Start()
    {
        hp = 35;
        speed = 1.5f;
        damage = 15;
        exp = 7;
        isSlow = false;
        if (isBoss == true)
        {
            base.InitializeBossStats();
        }
        currentHp = hp;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
}
