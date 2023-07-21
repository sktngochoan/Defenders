using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HasagiSkill : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    public int hasagi_level = 1;
    public float hasagi_coolDown = 2f;
    public float hasagi_damage = 9999f;
    public float hasagi_distance = 10f;
    public float hasagi_speed = 10f;
    public bool isCooldown = false;
    private GameObject hasagi;
    public Image cooldownImage;
    public TextMeshProUGUI timeText;
    private float currentCooldownTime = 0f;

    public void Start()
    {
        cooldownImage.fillAmount = 0;
        timeText.gameObject.SetActive(false);
        cooldownImage.enabled = false;
    }
    public void Update()
    {
        if (isCooldown)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 0)
            {
                currentCooldownTime = 0;
                isCooldown = false;
                timeText.gameObject.SetActive(false);
                cooldownImage.enabled = true;
            }
            UpdateCooldownUI();
        }
    }
    public void ActivateHasagiSkill()
    {
        if (isCooldown)
        {
            return;
        }
        var horizontalMoveJoystick = joystick.Horizontal;
        var verticalMoveJoystick = joystick.Vertical;

        if (horizontalMoveJoystick == 0 && verticalMoveJoystick == 0)
        {
            var prefab = Resources.Load("HasagiSkills") as GameObject;
            hasagi = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Rigidbody2D ballRigidbody = hasagi.GetComponent<Rigidbody2D>();
            ballRigidbody.AddForce(Vector3.right * hasagi_speed, ForceMode2D.Impulse);
        }
        else
        {
            var prefab = Resources.Load("HasagiSkills") as GameObject;
            Vector2 forceDirection = new Vector2(horizontalMoveJoystick, verticalMoveJoystick).normalized;
            hasagi = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Rigidbody2D ballRigidbody = hasagi.GetComponent<Rigidbody2D>();
            ballRigidbody.AddForce(forceDirection * hasagi_speed, ForceMode2D.Impulse);
        }
        StartCooldown();
    }
    public void UpdateHasagiSkill()
    {
        if (hasagi_coolDown > 0.5f)
        {
            hasagi_coolDown = hasagi_coolDown - 0.5f;
        }
    }

    private void StartCooldown()
    {
        isCooldown = true;
        currentCooldownTime = hasagi_coolDown;
        timeText.gameObject.SetActive(true);
        UpdateCooldownUI();
        cooldownImage.enabled = true;

        Invoke("HasagiFinishCooldown", hasagi_coolDown);
    }
    private void UpdateCooldownUI()
    {
        float fillAmount = currentCooldownTime / hasagi_coolDown;
        cooldownImage.fillAmount = fillAmount;
        timeText.text = Mathf.RoundToInt(currentCooldownTime).ToString();
    }

    private void HasagiFinishCooldown()
    {
        isCooldown = false;
        Destroy(hasagi);
        cooldownImage.enabled = false;
        timeText.gameObject.SetActive(false);
    }
}