using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SurvivalTimer : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool timeIsRunning = false;
    public TMP_Text timeText;
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if(timeRemaining >= 0) 
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining); 
            }
        }
    }
    void DisplayTime(float timeDisplay)
    {
        timeDisplay += 1;
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);

        timeText.text = string.Format("{00:00} : {01:00}", minutes, seconds);
    }
}
