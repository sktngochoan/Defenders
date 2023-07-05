using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public GameObject[] spawnPoints;
    public float spawnInterval = 2f; 
    public int initialPoolSize = 20; 
    public int maxPoolSize = 50; 
    private List<GameObject>[] enemyPool; 
    private readonly Transform[] _factoryBuildings = new Transform[1];

    private void Start()
    {
        AssignFactoryToBuilding();
        InitializePool();
        StartCoroutine(SpawnEnemies());
    }
    public void AssignFactoryToBuilding()
    {
        Transform turretTransform = null;
        CreateFactory(0, spawnPoints[0], out turretTransform);
       // CreateFactory(1, spawnPoints[1], out turretTransform);
      //  CreateFactory(2, spawnPoints[2], out turretTransform);
      //  CreateFactory(3, spawnPoints[3], out turretTransform);

        int randomEnemy = Random.Range(0, 1);
        if (randomEnemy == 0)
        {
            TowerEnemyFactory miniTurretFactory = turretTransform.gameObject.AddComponent<TowerEnemyFactory>();
            miniTurretFactory.TurretTransform = turretTransform;
        }
        else
        {
            PlayerEnemyFactory autoTurretFactory = turretTransform.gameObject.AddComponent<PlayerEnemyFactory>();
            autoTurretFactory.TurretTransform = turretTransform;
        }
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
            Transform spawnPoint = spawnPoints[spawnIndex].transform;
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject CreateEnemy(int enemyIndex)
    {
        int i = Random.Range(0, 1);
        // Create random
        GameObject enemy = new GameObject();
        if (i == 0)
        {
            enemy = _factoryBuildings[i].GetComponent<EnemyFactory>().CreateNormalEnemy();
        }
        else if (i == 1)
        {
            enemy = _factoryBuildings[i].GetComponent<EnemyFactory>().CreateBoss();
        }
        else
        {
            enemy = _factoryBuildings[i].GetComponent<EnemyFactory>().CreateSpecialEnemy();
        }
        enemy.SetActive(false);
        enemyPool[enemyIndex].Add(enemy);
        return enemy;
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
