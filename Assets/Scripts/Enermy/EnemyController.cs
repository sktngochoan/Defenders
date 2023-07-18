using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Animator animator;
    public SpriteRenderer sprite;
    private EnemyBaseState currentState;
    public Enemy enemy;
    public Rigidbody2D rb;
    public bool isHit;
    public bool hit;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        animator = gameObject.GetComponent<Animator>();
        isHit = enemy.isHit;
        hit = enemy.hit;
        currentState = new EnRunningState(this);
        currentState.EnterState();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        isHit = enemy.isHit;
        hit = enemy.hit;
        currentState.UpdateState();
        enemy.isHit = isHit;
        enemy.hit = hit;
    }

    public void ChangeState(EnemyBaseState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}