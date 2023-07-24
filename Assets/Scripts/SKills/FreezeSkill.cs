using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FreezeSkill : MonoBehaviour
{
    public int freeze_level = 1;
    public float freeze_coolDown = 10f;
    public float freeze_time = 3f;
    public bool isCooldown = false;
    private bool isFreezing = false;
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

    public void ActivateFreezeSkill()
    {
        if (!isCooldown && !isFreezing)
        {
            StartCoroutine(FreezeEnemies());
        }
    }

    public void UpdateFreezeSkill()
    {
        freeze_level += 1;
        freeze_time += 1;
        freeze_coolDown -= 0.5f;
    }

    private void UpdateCooldownUI()
    {
        float fillAmount = currentCooldownTime / freeze_coolDown;
        cooldownImage.fillAmount = fillAmount;
        timeText.text = Mathf.RoundToInt(currentCooldownTime).ToString();
    }

    private IEnumerator FreezeEnemies()
    {
        AudioManager.Play(AudioClipName.Frezze);
        isFreezing = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float speed_old = 0;
        if (enemies != null && enemies.Length > 0)
        {
            speed_old = enemies[0].GetComponent<Enemy>().speed;
        }

        // Dong bang
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().speed = 0;
                enemy.GetComponent<EnemyController>().animator.enabled = false;
            }
        }

        isCooldown = true;
        currentCooldownTime = freeze_coolDown;
        timeText.gameObject.SetActive(true);
        UpdateCooldownUI();
        cooldownImage.enabled = true;

        // Wait for freeze_time
        yield return new WaitForSeconds(freeze_time);

        // Binh thuong lai
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().speed = speed_old;
                enemy.GetComponent<EnemyController>().animator.enabled = true;
            }
        }

        // Update cooldown UI during the remaining cooldown time
        while (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            UpdateCooldownUI();
            yield return null;
        }

        isFreezing = false;
        isCooldown = false;
        timeText.gameObject.SetActive(false);
        cooldownImage.enabled = false;
    }

}
