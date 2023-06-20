using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private VariableJoystick joystick;

    private float moveDistance;
    void Start()
    {
        PlayerEntity playerEntity = GetComponent<PlayerEntity>();
        joystick.gameObject.SetActive(true);
        moveDistance = playerEntity.Speed;
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
}
