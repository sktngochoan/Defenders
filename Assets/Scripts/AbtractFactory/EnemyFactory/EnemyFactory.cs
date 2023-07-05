using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public Transform TurretTransform;
    public abstract GameObject CreateNormalEnemy();
    public abstract GameObject CreateSpecialEnemy();
    public abstract GameObject CreateBoss();
}
