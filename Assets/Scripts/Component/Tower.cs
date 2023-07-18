using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public static Tower instance;
    public float hp;
    public float currentHp;
    public Slider healthSlider;
    public static Tower Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Tower>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(Tower).Name);
                    instance = singletonObject.AddComponent<Tower>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as Tower;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        healthSlider.value = 1;
        currentHp = hp;
    }

    public void OnhitTower(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar(currentHp, hp);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}