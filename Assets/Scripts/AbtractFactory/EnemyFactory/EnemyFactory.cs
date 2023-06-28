using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public Transform TurretTransform;
    public abstract void CreateNormalEnemy();
    public abstract void CreateSpecialEnemy();
    public abstract void CreateBoss();
}
