using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] public VariableJoystick joystick;
    public GameObject swordRange;

    private Rigidbody2D rigid2D;
    public Animator animator;
    public SpriteRenderer sprite;
    private BaseState currentState;
    private float moveDistance;
    void Start()
    {
        PlayerEntity playerEntity = GetComponent<PlayerEntity>();
        joystick.gameObject.SetActive(true);
        moveDistance = playerEntity.Speed;

        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        currentState = new IdleState(this);
        currentState.EnterState();

        
    }
    void Update()
    {
        float hori = 0;
        float verti = 0;
        if(joystick.Horizontal != 0)
        {
            hori = joystick.Horizontal > 0 ? 1 : -1;
        }
        if (joystick.Vertical != 0)
        {
            verti = joystick.Vertical > 0 ? 1 : -1;
        }
        float moveX = hori * moveDistance;
        float moveY = verti * moveDistance;
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
        rigid2D.velocity = new Vector2(move * moveDistance, rigid2D.velocity.y);

        if (move > 0)
        {
            if (swordRange.transform.localScale.x < 0)
            {
                swordRange.transform.localScale *= -1;
                swordRange.transform.localPosition = new Vector3(swordRange.transform.localPosition.x * -1, swordRange.transform.localPosition.y, 0);
            }
            sprite.flipX = false;
            
        }
        if (move < 0)
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
}
