using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] public VariableJoystick joystick;
    [SerializeField] private FloatingHealthBar healthBar;
    [SerializeField] private FloatingExpBar expBar;
    public PlayerEntity playerEntity;
    public GameObject swordRange;

    private Rigidbody2D rigid2D;
    public Animator animator;
    public SpriteRenderer sprite;
    private BaseState currentState;
    private float speed;

    private Vector2 move;
    void Start()
    {
        playerEntity = GetComponent<PlayerEntity>();
        joystick.gameObject.SetActive(true);
        speed = playerEntity.Speed;

        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        currentState = new IdleState(this);
        currentState.EnterState();

        
    }
    void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
        Vector2 direction = new Vector2(move.x, move.y).normalized;
        rigid2D.MovePosition(rigid2D.position + direction * speed * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        currentState.UpdateState();
        float moveVec = joystick.Horizontal;
        rigid2D.velocity = new Vector2(moveVec * speed, rigid2D.velocity.y);

        if (moveVec > 0)
        {
            if (swordRange.transform.localScale.x < 0)
            {
                swordRange.transform.localScale *= -1;
                swordRange.transform.localPosition = new Vector3(swordRange.transform.localPosition.x * -1, swordRange.transform.localPosition.y, 0);
            }
            sprite.flipX = false;

        }
        if (moveVec < 0)
        {
            if (swordRange.transform.localScale.x > 0)
            {
                swordRange.transform.localScale *= -1;
                swordRange.transform.localPosition = new Vector3(swordRange.transform.localPosition.x * -1, swordRange.transform.localPosition.y, 0);
            }
            sprite.flipX = true;
        }
    }
    
    public void ChangeState(BaseState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
    public void changeExp()
    {
        expBar.UpdateExpBar(playerEntity.CurrentExp,playerEntity.Exp);
    }

    public void changeHp()
    {
        healthBar.UpdateHealthBar(playerEntity.CurrentHp, playerEntity.HP);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyEntity = col.gameObject.GetComponent<Enemy>();
            playerEntity.CurrentHp = playerEntity.CurrentHp - enemyEntity.damage;
            changeHp();
            //playerEntity.HP -= enemyEntity.damage;

        }
    }
}
