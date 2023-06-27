using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Client : MonoBehaviour
{
    private const int MaxNumberOfFactoryBuildings = 4;
    private const string TowerEnemy = "TowerEnemy";
    private const string PlayerEnemy = "PlayerEnemy";
    private readonly Transform[] _factoryBuildings = new Transform[MaxNumberOfFactoryBuildings];
    public GameObject FacA;
    public GameObject FacB;
    public GameObject FacC;
    public GameObject FacD;
    private void Start()
    {
        InvokeRepeating(nameof(CreateEnemy), 3, 5f);
    }

    public void CreateEnemy()
    {
        for (var i = 0; i < MaxNumberOfFactoryBuildings; i++)
        {
            // Skip if the no turret
            if (_factoryBuildings[i] == null || _factoryBuildings[i].GetComponent<EnemyFactory>() == null) continue;

            // Create random bullet
            if (Random.Range(0, 2) != 0)
            {
                _factoryBuildings[i].GetComponent<EnemyFactory>().CreateNormalEnemy();
            }
            else
            {
                _factoryBuildings[i].GetComponent<EnemyFactory>().CreateSpecialEnemy();
            }
        }
    }

    // Trigger when user click button
    public void AssignFactoryToBuilding()
    {
        Transform turretTransform = null;
        // Position
        int randomPosition = Random.Range(0, 4);
        if(randomPosition == 0)
        {
            CreateFactory(0, FacA, out turretTransform);
        }
        else if(randomPosition == 1)
        {
            CreateFactory(1, FacB, out turretTransform);
        }
        else if(randomPosition == 2)
        {
            CreateFactory(2, FacC, out turretTransform);
        }
        else
        {
            CreateFactory(3, FacD, out turretTransform);
        }
        if (turretTransform == null)
        {
            Debug.Log("Could not initialize or load factory transform");
            return;
        }
        // 
        int randomEnemy = Random.Range(0, 2);
        if(randomEnemy == 0)
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

    private Transform CreateFactoryBuilding(GameObject Fac)
    {
        Transform newFactory = Instantiate(Fac.transform, new Vector2(Fac.transform.position.x, Fac.transform.position.y), Quaternion.identity);
        return newFactory;
    }

    private void CreateFactory(int arrayPosition,GameObject Factory, out Transform factoryBuildingTransform)
    {
        if (_factoryBuildings[arrayPosition] == null)
        {
            // Create transforms for factory building
            _factoryBuildings[arrayPosition] = CreateFactoryBuilding(Factory);
            factoryBuildingTransform = _factoryBuildings[arrayPosition];
        }
        else
        {
            // Reuse existing transforms
            factoryBuildingTransform = _factoryBuildings[arrayPosition];
        }
    }
}
