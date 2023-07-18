using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseMenuUI;
    public string path;
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = true;
    }

    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
        filePath = Path.Combine(Application.persistentDataPath, "data", FolderName);
        if (FileName != "")
            filePath = Path.Combine(filePath, FileName + ".json");
        return filePath;
    }
    public void Save()
    {
        // Player
        string playerFilePath = "";
        playerFilePath = GetFilePath("data", "playerData");
        if (!Directory.Exists(Path.GetDirectoryName(playerFilePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(playerFilePath));
        }
        GameObject Player = GameObject.Find("Hero");
        PlayerEntity playerEntity = Player.GetComponent<PlayerEntity>();
        PlayerModel playerModel = new PlayerModel();
        playerModel.lv = playerEntity.Level;
        playerModel.currentExp = playerEntity.CurrentExp;
        playerModel.currentHp = playerEntity.CurrentHp;
        playerModel.position = Player.transform.position;
        string playerJson = JsonUtility.ToJson(playerModel, true);
        File.WriteAllText(playerFilePath, playerJson);

        // Enemy
        string enemyFilePath = "";
        enemyFilePath = GetFilePath("data", "enemyData");
        if (!Directory.Exists(Path.GetDirectoryName(enemyFilePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(playerFilePath));
        }
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        List<EnemyModel> enemyModelList = new List<EnemyModel>();
        foreach (GameObject item in enemyList)
        {
            EnemyModel e = new EnemyModel();
            Enemy enemy = item.GetComponent<Enemy>();
            e.position = item.transform.position;
            e.currentHp = enemy.currentHp;
            e.typePool = enemy.typePool;

            enemyModelList.Add(e);
        }
        string enemyJson = JsonHelper.ToJson<EnemyModel>(enemyModelList.ToArray());
        File.WriteAllText(enemyFilePath, enemyJson);
        // Other
        PlayerPrefs.SetFloat("timeRemain", SurvivalTimer.Instance.timeRemaining);
    }

    public void Quiz()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
