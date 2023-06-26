using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{
    public override EnemyType GetEnemyType()
    {
        return EnemyType.TowerEnemy;
    }
}
