﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashSkill : MonoBehaviour
{
    // Dash
    [SerializeField]
    private float dashDistance = 4f;
    [SerializeField]
    private float dashCooldown = 5f;
    [SerializeField] public VariableJoystick joystick;
    [SerializeField]
    private LayerMask dashLayerMask;
    private bool isDashing;
    private Rigidbody2D rigid2D;
    public GameObject player;
    void Start()
    {
        rigid2D = player.GetComponentInParent<Rigidbody2D>();
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dash();
        }
    }
    public void dash()
    {
        if (isDashing)
        {
            return;
        }
        var horizontalMoveJoystick = joystick.Horizontal;
        var verticalMoveJoystick = joystick.Vertical;
        var dashDirection = new Vector2(horizontalMoveJoystick, verticalMoveJoystick).normalized;
        if (horizontalMoveJoystick == 0 && verticalMoveJoystick == 0)
        {
            dashDirection = Vector2.right;
        }
        var dashTargetPosition = new Vector2(player.transform.position.x, player.transform.position.y) + dashDirection * dashDistance;
        RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, dashDirection, dashDistance, dashLayerMask);
        if (raycastHit2d.collider != null)
        {
            dashTargetPosition = raycastHit2d.point;
            if (raycastHit2d.point.x > 0 && raycastHit2d.point.y > -2)
            {
                dashTargetPosition = raycastHit2d.point - new Vector2(2, 2);
            }
            else if (raycastHit2d.point.x < 0 && raycastHit2d.point.y > -2)
            {
                dashTargetPosition = raycastHit2d.point - new Vector2(-1, 1);
            }
        }
        var currentVelocity = rigid2D.velocity;
        while (Vector2.Distance(player.transform.position, dashTargetPosition) > 0.1f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, dashTargetPosition, 0.1f);
        }
        rigid2D.velocity = currentVelocity;
        StartCooldown();
    }
    private void StartCooldown()
    {
        isDashing = true;
        Invoke("DashFinishCooldown", dashCooldown);
    }
    private void DashFinishCooldown()
    {
        isDashing = false;
    }
}

