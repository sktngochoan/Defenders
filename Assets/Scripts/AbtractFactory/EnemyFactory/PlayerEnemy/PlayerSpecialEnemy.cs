using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialEnemy : PlayerEnemy
{
    public float explosion_damage;
    public float explosion_distance;
    public float explosion_time;
    void Start()
    {
        explosion_damage = 15;
        explosion_distance = 3;
        explosion_time = 2;
        hp = 50;
        speed = 1.5f;
        damage = 5;
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

