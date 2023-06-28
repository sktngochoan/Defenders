using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Enemy : MonoBehaviour
{
    public int exp;
    public float hp;
    public float speed;
    public float damage;
    public bool isSlow;
    public float currentHp;
    public bool isBoss = false;
    public FloatingHealthBar healthBar;
    public Rigidbody2D rb;
    public enum EnemyType
    {
        PlayerEnemy,
        TowerEnemy
    }
    public abstract EnemyType GetEnemyType();
    public abstract void InitializeBossStats();
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.CompareTag("Wall"))
        //{
        //    Destroy(gameObject);
        //}
    }

    public void onHit(Transform playerTransform)
    {
        isDead();
        UpdateHealthBar();
        Vector2 knockback_direaction = gameObject.transform.position - playerTransform.position;
        transform.position = new Vector2(transform.position.x + knockback_direaction.x / 2, transform.position.y);
    }
    private void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(currentHp, hp);
    }
    public void isDead()
    {
        if(currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
