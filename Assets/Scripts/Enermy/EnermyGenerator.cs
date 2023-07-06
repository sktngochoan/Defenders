using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyGenerator : MonoBehaviour
{
    public static EnermyGenerator Instance;
    public GameObject[] enemyPrefabs; 
    public GameObject[] spawnPoints;
    public float spawnInterval = 2f; 
    public int initialPoolSize = 20; 
    public int maxPoolSize = 50; 
    private List<GameObject>[] enemyPool;
    private readonly Transform[] _factoryBuildings = new Transform[4];

    private void Awake()
    {
        Instance = this;
        AssignFactoryToBuilding();
        InitializePool();
        StartCoroutine(SpawnEnemies());
    }
    public void AssignFactoryToBuilding()
    {
        for(int i = 0;i<4;i++)
        {
            Transform turretTransform = null;
            CreateFactory(i, spawnPoints[i], out turretTransform);
            // sửa lại đoạn này thành 0 - 2
            // Tạo ra 4 loại
            int randomEnemy = Random.Range(0, 1);
            if (randomEnemy == 0)
            {
                TowerEnemyFactory TowerFactory = turretTransform.gameObject.AddComponent<TowerEnemyFactory>();
                TowerFactory.TurretTransform = turretTransform;
            }
            else
            {
                PlayerEnemyFactory PlayerFactory = turretTransform.gameObject.AddComponent<PlayerEnemyFactory>();
                PlayerFactory.TurretTransform = turretTransform;
            }
        }
        
        //CreateFactory(0, spawnPoints[0], out turretTransform);
      //  CreateFactory(2, spawnPoints[2], out turretTransform);
      //  CreateFactory(3, spawnPoints[3], out turretTransform);

        
    }

    private void InitializePool()
    {
        enemyPool = new List<GameObject>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPool[i] = new List<GameObject>();

            for (int j = 0; j < initialPoolSize; j++)
            {
                CreateEnemy(i);
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            List<GameObject> pool = enemyPool[enemyIndex];
            GameObject enemy = pool.Find(e => !e.activeInHierarchy);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.typePool = enemyIndex;
            if (enemy == null)
            {
                if (pool.Count < maxPoolSize)
                {
                    enemy = CreateEnemy(enemyIndex);
                }
                else
                {
                    yield return null;
                    continue;
                }
            }
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            //Transform spawnPoint = spawnPoints[spawnIndex].transform;
            enemy.transform.position = spawnPoints[spawnIndex].transform.position;
            enemy.transform.rotation = spawnPoints[spawnIndex].transform.rotation;
            enemy.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private GameObject CreateEnemy(int enemyIndex)
    {
        int typeE = Random.Range(0, 2);
        int ranFac = Random.Range(0, _factoryBuildings.Length);
        // Create random
        GameObject enemy = null;
        if (typeE == 0)
        {
            enemy = _factoryBuildings[1].GetComponent<EnemyFactory>().CreateNormalEnemy();
        }
        else if (typeE == 1)
        {
            enemy = _factoryBuildings[1].GetComponent<EnemyFactory>().CreateSpecialEnemy();
        }
        else
        {
            enemy = _factoryBuildings[1].GetComponent<EnemyFactory>().CreateBoss();
        }
        enemy.SetActive(false);
        enemyPool[enemyIndex].Add(enemy);
        return enemy;
    }
    public void ReturnEnemy(GameObject enemy,int enemyIndex)
    {
        enemy.SetActive(false);
        enemyPool[enemyIndex].Add(enemy);
    }
    private Transform CreateFactoryBuilding(GameObject Fac)
    {
        Transform newFactory = Instantiate(Fac.transform, new Vector2(Fac.transform.position.x, Fac.transform.position.y), Quaternion.identity);
        return newFactory;
    }

    private void CreateFactory(int arrayPosition, GameObject Factory, out Transform factoryBuildingTransform)
    {
        if (_factoryBuildings[arrayPosition] == null)
        {
            _factoryBuildings[arrayPosition] = CreateFactoryBuilding(Factory);
            factoryBuildingTransform = _factoryBuildings[arrayPosition];
        }
        else
        {
            factoryBuildingTransform = _factoryBuildings[arrayPosition];
        }
    }
}
