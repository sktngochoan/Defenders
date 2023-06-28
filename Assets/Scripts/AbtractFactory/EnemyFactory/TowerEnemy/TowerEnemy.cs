using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{
    public override EnemyType GetEnemyType()
    {
        return EnemyType.TowerEnemy;
    }
    public override void InitializeBossStats()
    {
        hp *= 3;
        damage *= 2;
        exp *= 2;
    }
}
