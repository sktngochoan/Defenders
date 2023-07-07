using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

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
        }
    }
    
    private void loadData()
    {
        // Player
        string filePath = "";
        filePath = GetFilePath("data", "playerData");
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
        string playerJson = File.ReadAllText(filePath);
        PlayerModel playerModel = JsonUtility.FromJson<PlayerModel>(playerJson);

        GameObject Player = GameObject.Find("Hero");
        PlayerEntity playerEntity = Player.GetComponent<PlayerEntity>();
        Debug.Log(playerModel.position);
        playerEntity.UpdateEntityWithLv(playerModel.lv, playerModel.currentHp, playerModel.currentExp);
        Player.transform.position = playerModel.position;
        // Enemy
        //string enemyJson = File.ReadAllText(Application.dataPath + "/EnemyDataFile.json");
        //List<EnemyModel> enemyModels = JsonHelper.FromJson<EnemyModel>(enemyJson).ToList();
        //EnermyGenerator.Instance.SpawnEnemyOnLoad(enemyModels);
        //GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject enemyObject in enemyList)
        //{
        //    Enemy enemy = enemyObject.GetComponent<Enemy>();
        //    // wait until slider created
        //    StartCoroutine(UpdateHealthBarCoroutine(enemy));
        //}
    }
    private IEnumerator UpdateHealthBarCoroutine(Enemy enemy)
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

    private static string GetFilePath(string FolderName, string FileName = "")
    {
        string filePath;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        // mac
        filePath = Path.Combine(Application.streamingAssetsPath, ("data/" + FolderName));

        if (FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".txt"));
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // windows
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if (FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".json"));
#elif UNITY_ANDROID
        // android
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".json"));
#elif UNITY_IOS
        // ios
        filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));

        if(FileName != "")
            filePath = Path.Combine(filePath, (FileName + ".txt"));
#endif
        return filePath;
    }
}
