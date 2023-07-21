using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealSkill : MonoBehaviour
{
    public int heal_level = 1;
    public float heal_coolDown = 10f;
    public bool isCooldown = false;

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
                cooldownImage.enabled = false;
            }
            UpdateCooldownUI();
        }
    }

    public void ActivateHealSkill()
    {
        if (!isCooldown)
        {
            StartCoroutine(UseHealSkills());
        }
    }

    public void UpdateHealSkill()
    {
        heal_level += 1;
        if (heal_coolDown > 0.5f)
        {
            heal_coolDown -= 0.5f;
        }
    }

    private void UpdateCooldownUI()
    {
        float fillAmount = currentCooldownTime / heal_coolDown;
        cooldownImage.fillAmount = fillAmount;
        timeText.text = Mathf.RoundToInt(currentCooldownTime).ToString();
    }

    private IEnumerator UseHealSkills()
    {
        isCooldown = true;
        currentCooldownTime = heal_coolDown;
        timeText.gameObject.SetActive(true);
        UpdateCooldownUI();
        cooldownImage.enabled = true;
        AudioManager.Play(AudioClipName.Heal);
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        PlayerController controller = hero.GetComponent<PlayerController>();
        if (hero != null)
        {
            PlayerEntity playerEntity = hero.GetComponent<PlayerEntity>();
            if (playerEntity != null)
            {
                if (playerEntity.CurrentHp < 100)
                {
                    playerEntity.CurrentHp += 10;
                    if (playerEntity.CurrentHp > 100)
                    {
                        playerEntity.CurrentHp = 100;
                    }
                    controller.changeHp();
                }
            }
            else
            {
                Debug.LogWarning("PlayerEntity component not found on Hero GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Hero GameObject not found.");
        }

        // Wait for the remaining cooldown time
        while (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            UpdateCooldownUI();
            yield return null;
        }

        isCooldown = false;
        timeText.gameObject.SetActive(false);
        cooldownImage.enabled = false;
    }
}
