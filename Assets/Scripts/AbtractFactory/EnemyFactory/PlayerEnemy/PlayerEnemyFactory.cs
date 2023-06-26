using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyFactory : EnemyFactory
{
    private const string NormalEnemy = "Enermy3";
    private const string SpecialEnemy = "Enermy4";

    public override void CreateNormalEnemy()
    {
        var factoryTransformPosition = TurretTransform.transform.position;
        var normalEnemy = Resources.Load(NormalEnemy) as GameObject;
        if (normalEnemy != null)
        {
            var SlowBullet = Instantiate(normalEnemy.transform, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
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
            var FastBullet = Instantiate(specialEnemy.transform, new Vector2(factoryTransformPosition.x, factoryTransformPosition.y), Quaternion.identity);
        }
        else
        {
            throw new System.ArgumentException(SpecialEnemy + "could not be found inside or loaded from Resources folder");
        }
    }
}
