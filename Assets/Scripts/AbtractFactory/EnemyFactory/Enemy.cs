using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int exp;
    public float hp;
    public float speed;
    public float damage;
    public bool isSlow;

    public Transform towerTransform;
    public Transform playerTransform;
    public enum EnemyType
    {
        PlayerEnemy,
        TowerEnemy
    }
    public void Start()
    {
        this.towerTransform = towerTransform;
        this.playerTransform = playerTransform;

    }
    public abstract EnemyType GetEnemyType();
    public abstract void Movement();
    void Update()
    {
        Movement();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.CompareTag("Wall"))
        //{
        //    Destroy(gameObject);
        //}

    }
}
