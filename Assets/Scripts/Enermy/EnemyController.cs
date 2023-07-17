using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Animator animator;
    public SpriteRenderer sprite;
    private EnemyBaseState currentState;
    private Enemy enemy;
    public bool isHit;
    public bool hit;
    public GameObject player;
    public float distanceThreshold = 2f;
    public Rigidbody2D rb;
    public float force;
    public Vector3 initialDirection;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        isHit = enemy.isHit;
        hit = enemy.hit;
        currentState = new EnRunningState(this);
        currentState.EnterState();

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Hero");

        //initialDirection = (player.transform.position - transform.position).normalized;


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        isHit = enemy.isHit;
        hit = enemy.hit;
        currentState.UpdateState();
    }

    public void ChangeState(EnemyBaseState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            // Chuy?n tr?ng thái c?a Enemy thành EnOnhitState
            //ChangeState(new EnOnhitState(this));
            enemy.hit = true;
        }
    }
}
