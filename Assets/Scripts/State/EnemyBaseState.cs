using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
    protected EnemyController enemy;
    // Start is called before the first frame update
    public EnemyBaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
