using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower instance;
    public float hp;
    public float currentHp;

    public static Tower Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Tower>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(Tower).Name);
                    instance = singletonObject.AddComponent<Tower>();
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
            instance = this as Tower;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentHp = hp;
    }

    public void OnhitTower(float damage)
    {
        currentHp -= damage;
    }
}
