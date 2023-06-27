using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemy : Enemy
{
    public float moveSpeed = 5f; // Tốc độ di chuyển của kẻ thù
    public float attackRange = 5f; // Khoảng cách tấn công

    private bool isAttacking = false; // Kiểm tra xem kẻ thù đang tấn công hay không

    public override EnemyType GetEnemyType()
    {
        return EnemyType.TowerEnemy;
    }

    public void Start()
    {
        base.Start();
    }

    public override void Movement()
    {

        if (towerTransform != null && !isAttacking)
        {
            // Tính toán vị trí chính giữa của trụ
            Vector3 targetPosition = towerTransform.position + towerTransform.forward * 0.5f; // Vị trí chính giữa trụ

            // Di chuyển kẻ thù đến vị trí chính giữa trụ
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Kiểm tra khoảng cách đến trụ để tấn công
            if (Vector3.Distance(transform.position, targetPosition) <= attackRange)
            {
                isAttacking = true;
            }
        }
    }
}
