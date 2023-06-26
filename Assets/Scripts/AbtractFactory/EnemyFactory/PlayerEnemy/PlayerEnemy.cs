using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : Enemy
{
    public override EnemyType GetEnemyType()
    {
        return EnemyType.PlayerEnemy;
    }
}
