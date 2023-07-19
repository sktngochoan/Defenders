using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : MonoBehaviour
{
    public int freeze_level = 1;
    public float freeze_coolDown = 15f;
    public float freeze_time = 3f;
    public bool isCooldown = false;
    private bool isFreezing = false;

    public void ActivateFreezeSkill()
    {
        if (!isCooldown && !isFreezing)
        {
            StartCoroutine(FreezeEnemies());

        }
    }
    public void UpdateFreeSkill()
    {
        freeze_level = freeze_level + 1;
        freeze_time = freeze_time + 1;
        freeze_coolDown = freeze_coolDown - 0.5f;
    }

    private IEnumerator FreezeEnemies()
    {
        isFreezing = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float speed_old = 0;
        if (enemies != null)
        {
             speed_old = enemies[0].GetComponent<Enemy>().speed;
        }
        //Dong bang
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {              
                enemy.GetComponent<Enemy>().speed = 0; 
                enemy.GetComponent<EnemyController>().animator.enabled = false;
            }
        }
        yield return new WaitForSeconds(freeze_time); 
        //Binh thuong lai
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {

                enemy.GetComponent<Enemy>().speed = speed_old;
                enemy.GetComponent<EnemyController>().animator.enabled = true;
            }
        }
        isFreezing = false;
        isCooldown = true;
        yield return new WaitForSeconds(freeze_coolDown);
        isCooldown = false;
    }
}
