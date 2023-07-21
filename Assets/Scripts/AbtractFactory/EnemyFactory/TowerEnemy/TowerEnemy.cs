using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerEnemy : Enemy
{
    public float attackRange = 2f;
    private bool isAttacking = false;
    public Transform towerTransformx;

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

    public override void InitializeOnLoad()
    {
        currentHp = currentHpOnLoad;
    }
    public override void Movement()
    {
        towerTransformx = GameObject.FindGameObjectWithTag("Tower").transform;
        Vector3 targetPosition = towerTransformx.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position.x < towerTransformx.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public override bool checkDistance()
    {
        if (towerTransformx == null)
        {
            return false;
        }
        float distance = Vector2.Distance(transform.position, towerTransformx.position);
        if (distance < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Attack()
    {
        hit = true;
        AudioManager.Play(AudioClipName.EnemyAttack);
        Tower.Instance.OnhitTower(damage);
    }
}