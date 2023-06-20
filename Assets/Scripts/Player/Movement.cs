using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerTransform;
    [SerializeField] public VariableJoystick joystick;

    public Rigidbody2D rigidbody2D;
    public Animator animator;
    public SpriteRenderer sprite;

    private BaseState currentState;

    private float moveDistance;
    void Start()
    {
        PlayerEntity playerEntity = GetComponent<PlayerEntity>();
        joystick.gameObject.SetActive(true);
        moveDistance = playerEntity.Speed;

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        currentState = new IdleState(this);
        currentState.EnterState();
    }
    void Update()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        var verticalMove = Input.GetAxis("Vertical");

        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;

        float moveX = 0;

        if (horizontalMove != 0)
        {
            moveX = horizontalMove < 0 ? -moveDistance : moveDistance;
        }

        float moveY = 0;

        if (verticalMove != 0)
        {
            moveY = verticalMove < 0 ? -moveDistance : moveDistance;
        }

        if (moveX != 0 || moveY != 0)
        {
            var pos = playerTransform.position;
            playerTransform.position = new Vector3(pos.x + moveX, pos.y + moveY, pos.z);
        }
    }
    private void FixedUpdate()
    {
        currentState.UpdateState();

        float move = joystick.Horizontal;
        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);

        if (move > 0)
        {
            sprite.flipX = false;
        }
        if (move < 0)
        {
            sprite.flipX = true;
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
