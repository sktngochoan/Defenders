using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyFactory : EnemyFactory
{
    private const string NormalEnemy = "Enermy1";
    private const string SpecialEnemy = "Enermy2";

    public override GameObject CreateNormalEnemy()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var normalEnemy = Resources.Load(NormalEnemy) as GameObject;
        if (normalEnemy != null)
        {
            GameObject NormalEnemy = Instantiate(normalEnemy, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            return NormalEnemy;
        }
        else
        {
            throw new System.ArgumentException(NormalEnemy + " could not be found inside or loaded from Resources folder");
        }
    }

    public override GameObject CreateSpecialEnemy()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var specialEnemy = Resources.Load(SpecialEnemy) as GameObject;
        if (specialEnemy != null)
        {
            GameObject SpecialEnemy = Instantiate(specialEnemy, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            return SpecialEnemy;
        }
        else
        {
            throw new System.ArgumentException(SpecialEnemy + " could not be found inside or loaded from Resources folder");
        }
    }

    public override GameObject CreateBoss()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var specialEnemy = Resources.Load(SpecialEnemy) as GameObject;
        var normalEnemy = Resources.Load(NormalEnemy) as GameObject;
        int random = Random.Range(0, 2);
        GameObject bossEnemy = specialEnemy;
        if (random == 1)
        {
            bossEnemy = normalEnemy;
        }

        if (specialEnemy != null || normalEnemy != null)
        {
            GameObject BossEnemy = Instantiate(bossEnemy, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            BossEnemy.transform.localScale *= 2;
            Enemy enemy = BossEnemy.GetComponent<Enemy>();
            enemy.isBoss = true;
            return BossEnemy;
        }
        else
        {
            throw new System.ArgumentException(SpecialEnemy + " could not be found inside or loaded from the Resources folder");
        }
    }

}
