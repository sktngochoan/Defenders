using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{
    public float attackRange = 5f; 

    private bool isAttacking = false; 

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
    public void Start()
    {
        base.Start();
    }
    public override void Movement()
    {
        Vector3 targetPosition = towerTransform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) <= attackRange)
        {
            isAttacking = true;
        }
    }
}
