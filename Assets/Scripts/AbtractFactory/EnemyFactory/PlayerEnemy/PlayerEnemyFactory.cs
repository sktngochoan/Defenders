using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyFactory : EnemyFactory
{
    private const string NormalEnemy = "Enermy3";
    private const string SpecialEnemy = "Enermy4";

   public override GameObject CreateNormalEnemy()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var normalEnemy = Resources.Load(NormalEnemy) as GameObject;
        if (normalEnemy != null)
        {
            GameObject slowBullet = Instantiate(normalEnemy, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            return slowBullet;
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
            GameObject fastBullet = Instantiate(specialEnemy, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            return fastBullet;
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
        var bossEnemy = specialEnemy;
        if (random == 1)
        {
            bossEnemy = normalEnemy;
        }

        if (specialEnemy != null || normalEnemy != null)
        {
            GameObject bossEnemyObject = Instantiate(bossEnemy, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
            bossEnemyObject.transform.localScale *= 2;
            Enemy enemy = bossEnemyObject.GetComponent<Enemy>();
            enemy.isBoss = true;
            return bossEnemyObject;
        }
        else
        {
            throw new System.ArgumentException(SpecialEnemy + " could not be found inside or loaded from Resources folder");
        }
    }
}
