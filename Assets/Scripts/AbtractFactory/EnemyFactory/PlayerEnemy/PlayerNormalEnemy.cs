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
        speed = 1f;
        damage = 10;
        exp = 7;
        isSlow = false;
        if (isBoss == true)
        {
            base.InitializeBossStats();
        }
        currentHp = hp;
        if (isLoad == true)
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
