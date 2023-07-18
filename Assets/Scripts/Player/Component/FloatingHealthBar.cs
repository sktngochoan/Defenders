using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingHealthBar : MonoBehaviour
{
    private Slider healthSlider;
    private void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.value = 1;
    }
    private void Update()
    {

    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}

