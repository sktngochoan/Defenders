using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyGenerator : MonoBehaviour
{
    public static EnermyGenerator Instance;
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints;
    public float spawnInterval = 5f;
    public int initialPoolSize = 20;
    public int maxPoolSize = 50;
    private List<GameObject>[] enemyPool;
    private readonly Transform[] factoryBuildings = new Transform[4];

    private void Awake()
    {
        Instance = this;
        AssignFactoryToBuilding();
        InitializePool();
        StartCoroutine(SpawnEnemies());
    }

    public void AssignFactoryToBuilding()
    {
        for (int i = 0; i < 4; i++)
        {
            Transform turretTransform = null;
            CreateFactory(i, spawnPoints[i], out turretTransform);

            int randomEnemy = Random.Range(0, 2);
            if (randomEnemy == 0)
            {
                TowerEnemyFactory towerFactory = turretTransform.gameObject.AddComponent<TowerEnemyFactory>();
                towerFactory.TurretTransform = turretTransform;
            }
            else
            {
                PlayerEnemyFactory playerFactory = turretTransform.gameObject.AddComponent<PlayerEnemyFactory>();
                playerFactory.TurretTransform = turretTransform;
            }
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

    public void SpawnEnemyOnLoad(List<EnemyModel> enemyModels)
    {
        foreach (EnemyModel model in enemyModels)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            List<GameObject> pool = enemyPool[model.typePool];
            GameObject enemy = pool.Find(e => !e.activeInHierarchy);
            if (enemy == null)
            {
                if (pool.Count < maxPoolSize)
                {
                    enemy = CreateEnemy(enemyIndex);
                }
                else
                {
                    continue;
                }
            }
            Enemy enemyEntity = enemy.GetComponent<Enemy>();
            enemy.transform.position = model.position;
            enemyEntity.currentHpOnLoad = model.currentHp;
            enemyEntity.isLoad = true;
            enemyEntity.typePool = model.typePool;
            enemy.SetActive(true);
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
            enemy.transform.position = spawnPoints[spawnIndex].transform.position;
            enemy.transform.rotation = spawnPoints[spawnIndex].transform.rotation;
            enemy.GetComponentInChildren<Canvas>().enabled = true;
            Enemy script = enemy.GetComponent<Enemy>();
            script.currentHp = script.hp;
            StartCoroutine(UpdateHealthBarEnemyCoroutine(script));
            enemy.SetActive(true);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private IEnumerator UpdateHealthBarEnemyCoroutine(Enemy enemy)
    {
        yield return new WaitUntil(() => enemy.healthBar != null);
        enemy.UpdateHealthBar();
    }
    private GameObject CreateEnemy(int enemyIndex)
    {
        GameObject enemyPrefab = enemyPrefabs[enemyIndex];
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false);
        enemyPool[enemyIndex].Add(enemy);
        return enemy;
    }

    public void ReturnEnemy(GameObject enemy, int enemyIndex)
    {
        enemy.SetActive(false);
        enemyPool[enemyIndex].Add(enemy);
    }

    private Transform CreateFactoryBuilding(GameObject factory)
    {
        Transform newFactory = Instantiate(factory.transform, factory.transform.position, Quaternion.identity);
        return newFactory;
    }

    private void CreateFactory(int arrayPosition, GameObject factory, out Transform factoryBuildingTransform)
    {
        if (factoryBuildings[arrayPosition] == null)
        {
            factoryBuildings[arrayPosition] = CreateFactoryBuilding(factory);
        }

        factoryBuildingTransform = factoryBuildings[arrayPosition];
    }
}