using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Prefabs của các loại quái vật
    public Transform[] spawnPoints; // Danh sách các điểm spawn

    public float spawnInterval = 2f; // Khoảng thời gian giữa mỗi lần sinh ra quái vật
    public int initialPoolSize = 20; // Số lượng quái vật ban đầu trong pool
    public int maxPoolSize = 50; // Số lượng quái vật tối đa trong pool

    private List<GameObject>[] enemyPool; // Pool chứa các quái vật

    private void Start()
    {
        // Khởi tạo pool và đặt các quái vật vào pool
        InitializePool();

        // Bắt đầu sinh ra quái vật
        StartCoroutine(SpawnEnemies());
    }

    private void InitializePool()
    {
        enemyPool = new List<GameObject>[enemyPrefabs.Length];

        // Khởi tạo pool cho từng loại quái vật
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPool[i] = new List<GameObject>();

            // Tạo và đặt các quái vật vào pool
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
            // Random loại quái vật
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            List<GameObject> pool = enemyPool[enemyIndex];

            // Tìm quái vật trong pool đã được deactivate
            GameObject enemy = pool.Find(e => !e.activeInHierarchy);

            if (enemy == null)
            {
                // Kiểm tra số lượng quái vật trong pool đã đạt tối đa chưa
                if (pool.Count < maxPoolSize)
                {
                    // Nếu chưa đạt tối đa, tạo một quái vật mới và thêm vào pool
                    enemy = CreateEnemy(enemyIndex);
                }
                else
                {
                    // Nếu đạt tối đa, không sinh ra quái vật mới
                    yield return null;
                    continue;
                }
            }

            // Random vị trí spawn
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // Reset lại trạng thái và vị trí của quái vật
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject CreateEnemy(int enemyIndex)
    {
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex]);
        enemy.SetActive(false);
        enemyPool[enemyIndex].Add(enemy);
        return enemy;
    }

}
