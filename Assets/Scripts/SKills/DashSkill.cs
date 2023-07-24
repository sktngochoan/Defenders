using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashSkill : MonoBehaviour
{
    [SerializeField] private float dashDistance = 4f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] public VariableJoystick joystick;
    [SerializeField] private LayerMask dashLayerMask;
    private bool isDashing;
    private Rigidbody2D rigid2D;
    public GameObject player;

    public Image cooldownImage;
    public TextMeshProUGUI timeText;
    private float currentCooldownTime = 0f;

    void Start()
    {
        rigid2D = player.GetComponentInParent<Rigidbody2D>();
        cooldownImage.fillAmount = 0;
        timeText.gameObject.SetActive(false);
        cooldownImage.enabled = false;
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
        AudioManager.Play(AudioClipName.Dash);
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
        currentCooldownTime = dashCooldown;
        timeText.gameObject.SetActive(true);
        UpdateCooldownUI();
        cooldownImage.enabled = true;

        StartCoroutine(DashCooldownRoutine());
    }

    private IEnumerator DashCooldownRoutine()
    {
        while (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            UpdateCooldownUI();
            yield return null;
        }

        isDashing = false;
        timeText.gameObject.SetActive(false);
        cooldownImage.enabled = false;
    }

    private void UpdateCooldownUI()
    {
        float fillAmount = currentCooldownTime / dashCooldown;
        cooldownImage.fillAmount = fillAmount;
        timeText.text = Mathf.RoundToInt(currentCooldownTime).ToString();
    }
}
