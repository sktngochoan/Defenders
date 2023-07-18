using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject updateSystem;
    public string path;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                    instance = singletonObject.AddComponent<GameManager>();
                }

                //DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as GameManager;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        updateSystem = GameObject.Find("UpdateSystem");
        updateSystem.SetActive(false);
        checkLoad();
    }
    private void checkLoad()
    {
        if (PlayerPrefs.GetInt("isLoad") == 1)
        {
            loadData();
            Time.timeScale = 1f;
        }
    }
    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
        filePath = Path.Combine(Application.persistentDataPath, "data", FolderName);
        if (FileName != "")
            filePath = Path.Combine(filePath, FileName + ".json");
        return filePath;
    }
    private void loadData()
    {
        // Player
        string playerFilePath = "";
        playerFilePath = GetFilePath("data", "playerData");
        if (!Directory.Exists(Path.GetDirectoryName(playerFilePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(playerFilePath));
        }
        else
        {
            string playerJson = File.ReadAllText(playerFilePath);
            PlayerModel playerModel = JsonUtility.FromJson<PlayerModel>(playerJson);

            GameObject Player = GameObject.Find("Hero");
            PlayerEntity playerEntity = Player.GetComponent<PlayerEntity>();
            playerEntity.UpdateEntityWithLv(playerModel.lv, playerModel.currentHp, playerModel.currentExp);
            PlayerController playerController = Player.GetComponent<PlayerController>();
            Player.transform.position = playerModel.position;
            StartCoroutine(UpdateHealthBarPlayerCoroutine(playerController));
            StartCoroutine(UpdateExpPlayerCoroutine(playerController));
        }

        // Enemy
        string enemyFilePath = "";
        enemyFilePath = GetFilePath("data", "enemyData");
        if (!Directory.Exists(Path.GetDirectoryName(enemyFilePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(enemyFilePath));
        }
        else
        {
            string enemyJson = File.ReadAllText(enemyFilePath);
            List<EnemyModel> enemyModels = JsonHelper.FromJson<EnemyModel>(enemyJson).ToList();
            EnermyGenerator.Instance.SpawnEnemyOnLoad(enemyModels);
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemyObject in enemyList)
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                // wait until slider created
                StartCoroutine(UpdateHealthBarEnemyCoroutine(enemy));
            }
        }
        // Other
        SurvivalTimer.Instance.timeRemaining = PlayerPrefs.GetFloat("timeRemain");
    }
    private IEnumerator UpdateExpPlayerCoroutine(PlayerController player)
    {
        yield return new WaitUntil(() => player.expBar != null);
        player.changeExp();
    }
    private IEnumerator UpdateHealthBarPlayerCoroutine(PlayerController player)
    {
        yield return new WaitUntil(() => player.healthBar != null);
        player.changeHp();
    }
    private IEnumerator UpdateHealthBarEnemyCoroutine(Enemy enemy)
    {
        yield return new WaitUntil(() => enemy.healthBar != null);
        enemy.UpdateHealthBar();
    }
    public void ActiveUpdateSystem()
    {
        updateSystem.SetActive(true);
    }

    public void DeActiveUpdateSystem()
    {
        updateSystem.SetActive(false);
    }

}
