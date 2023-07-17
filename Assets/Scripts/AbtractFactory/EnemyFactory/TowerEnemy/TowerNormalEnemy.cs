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
        if(isLoad == true)
        {
            base.InitializeOnLoad();
            isLoad = false;
        }
        rb = gameObject.GetComponent<Rigidbody2D>();

        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2f;
        timer.Run();
    }
}
