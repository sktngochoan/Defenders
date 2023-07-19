using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : MonoBehaviour
{
    public int heal_level = 1;
    public float heal_coolDown = 10f;
    public bool isCooldown = false;

    public void ActivateHealSkill()
    {
        if (isCooldown == false)
        {
            StartCoroutine(UseHealSkills());

        }
    }
    public void UpdateHealSkill()
    {
        heal_level = heal_level + 1;
        Debug.Log(heal_level);

        Debug.Log(heal_coolDown);

        if (heal_coolDown > 0.5f)
        {
            heal_coolDown = heal_coolDown - 0.5f;
            Debug.Log(heal_coolDown);
        }
    }
    IEnumerator UseHealSkills()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");

        if (hero != null)
        {
            PlayerEntity playerEntity = hero.GetComponent<PlayerEntity>();
            if (playerEntity != null)
            {
                if(playerEntity.CurrentHp < 100)
                {
                    playerEntity.CurrentHp += 10;
                }
                if(playerEntity.CurrentHp > 100)
                {
                    playerEntity.CurrentHp = 100;
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
        yield return new WaitForSeconds(heal_coolDown);

    }
}
