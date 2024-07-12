using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : StateBase
{
    protected EnemyController enemyController;
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void Init(IStateMachineOwner owner)
    {
        enemyController = owner as EnemyController;
    }

    public override void UnInit()
    {
       
    }

    public override void Update()
    {

    }
}
