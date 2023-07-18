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
    public float currentHpOnLoad;
    public bool isBoss = false;
    public bool isLoad = false;
    public bool isHit = false;
    public bool hit = false;
    public bool inRangeAttack = false;
    public int typePool;
    public FloatingHealthBar healthBar;
    public Rigidbody2D rb;
    public Timer timer;
    public enum EnemyType
    {
        PlayerEnemy,
        TowerEnemy
    }
    public abstract EnemyType GetEnemyType();
    public abstract void InitializeBossStats();
    public abstract void InitializeOnLoad();
    public abstract void Movement();
    public abstract bool checkDistance();
    public abstract void Attack();
    void Update()
    {
        if (checkDistance() == true)
        {
            if (timer.Finished)
            {
                Attack();
                timer.Run();
            }
        }
        else
        {
            Movement();
        }

    }
    public void onHit(Transform playerTransform)
    {
        isHit = true;
        isDead();
        UpdateHealthBar();
        Vector2 knockback_direaction = gameObject.transform.position - playerTransform.position;
        transform.position = new Vector2(transform.position.x + knockback_direaction.x / 2, transform.position.y);
    }
    public void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(currentHp, hp);
    }
    public void isDead()
    {
        if (currentHp <= 0)
        {
            StartCoroutine(ReturnEnemyAfterDelay());
            updateExp();
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    private void updateExp()
    {
        GameObject player = GameObject.Find("Hero");
        PlayerController controller = player.GetComponent<PlayerController>();
        PlayerEntity entity = player.GetComponent<PlayerEntity>();
        entity.CurrentExp += exp;
        if (entity.CurrentExp >= entity.Exp)
        {
            entity.UpdateLv();
        }
        controller.changeExp();
    }
    public void ApplyKnockback(Vector2 knockbackDirection, float knockbackForce)
    {
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }
    private IEnumerator ReturnEnemyAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        //EnermyGenerator.Instance.ReturnEnemy(gameObject, typePool);
        gameObject.SetActive(false);
    }
}