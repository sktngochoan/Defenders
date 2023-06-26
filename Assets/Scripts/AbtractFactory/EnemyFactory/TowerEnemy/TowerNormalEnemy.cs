using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNormalEnemy : TowerEnemy
{
    void Start()
    {
        hp = 50;
        speed = 5f;
        damage = 20;
        exp = 4;
        isSlow = false;
    }
}
