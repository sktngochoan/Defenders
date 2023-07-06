using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingExpBar : MonoBehaviour
{
    public Slider expSlider;
    private void Start()
    {
        //expSlider = GetComponentInChildren<Slider>();
        expSlider.value = 0;
    }
    private void Update()
    {

    }

    public void UpdateExpBar(float currentExp, float Exp)
    {
        expSlider.value = currentExp / Exp;
    }
}
