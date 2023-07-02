using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : Enemy
{
    public float attackRange = 5f; // Khoảng cách tấn công

    private bool isAttacking = false; // Kiểm tra xem kẻ thù đang tấn công hay không

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
    public void Start()
    {
        base.Start();
    }
    public override void Movement()
    {
        // if (playerTransform != null && !isAttacking)
        // {
        Vector3 targetPosition = playerTransform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) <= attackRange)
        {
            isAttacking = true;
        }
        // }
    }

}
