using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : Enemy
{
    public override EnemyType GetEnemyType()
    {
        return EnemyType.PlayerEnemy;
    }
    public override void InitializeBossStats()
    {
        hp *= 3;
        damage *= 2;
        exp *= 2;
    }
}
