using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public void Restart()
    {
        PlayerPrefs.SetInt("isLoad", 0);
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenuScene");
        Time.timeScale = 1f;
    }
}
