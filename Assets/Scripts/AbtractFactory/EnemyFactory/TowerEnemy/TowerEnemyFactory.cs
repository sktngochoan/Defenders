using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyFactory : EnemyFactory
{
    private const string NormalEnemy = "Enermy1";
    private const string SpecialEnemy = "Enermy2";

    public override void CreateNormalEnemy()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var normalEnemy = Resources.Load(NormalEnemy) as GameObject;
        if (normalEnemy != null)
        {
            var NormalEnemy = Instantiate(normalEnemy.transform, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
        }
        else
        {
            throw new System.ArgumentException(NormalEnemy + "could not be found inside or loaded from Resources folder");
        }
    }

    public override void CreateSpecialEnemy()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var specialEnemy = Resources.Load(SpecialEnemy) as GameObject;
        if (specialEnemy != null)
        {
            var SpecialEnemy = Instantiate(specialEnemy.transform, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
        }
        else
        {
            throw new System.ArgumentException(SpecialEnemy + "could not be found inside or loaded from Resources folder");
        }
    }

    public override void CreateBoss()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var specialEnemy = Resources.Load(SpecialEnemy) as GameObject;
        var normalEnemy = Resources.Load(NormalEnemy) as GameObject;
        int random = Random.Range(0, 2);
        var bossEnemy = specialEnemy;
        if (random == 1)
        {
            bossEnemy = normalEnemy;
        }

        if (specialEnemy != null || normalEnemy != null)
        {
            var BossEnemy = Instantiate(bossEnemy.transform, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            BossEnemy.localScale *= 2;
            Enemy enemy = BossEnemy.GetComponent<Enemy>();
            enemy.isBoss = true;
        }
        else
        {
            throw new System.ArgumentException(SpecialEnemy + "could not be found inside or loaded from Resources folder");
        }
    }
}
