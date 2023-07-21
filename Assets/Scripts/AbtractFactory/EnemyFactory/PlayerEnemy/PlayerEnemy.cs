using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : Enemy
{
    public float attackRange = 2.4f;
    public GameObject player;

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
    public override void InitializeOnLoad()
    {
        currentHp = currentHpOnLoad;
    }
    public override void Movement()
    {
        player = GameObject.FindGameObjectWithTag("Hero");
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position.x < player.transform.position.x)
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
        if (player == null)
        {
            return false;
        }
        float distance = Vector2.Distance(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 1));
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
        AudioManager.Play(AudioClipName.EnemyAttack);
        GameObject playerObject = GameObject.FindGameObjectWithTag("Hero");
        PlayerController script = playerObject.GetComponent<PlayerController>();
        script.onHitPlayer(damage, gameObject.transform.position);
        hit = true;
    }

}