using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    float idleTime;
    public override void EnterState()
    {
        base.EnterState();
        enemyController.PlayAnimation("Idle");
        idleTime = 0;
    }

    public override void Update()
    {
        base.Update();
        idleTime += Time.deltaTime;

        //´ýÔÚÔ­µØ
        if (enemyController.playerDistance > enemyController.fllowDistance)
        {
            if (idleTime >= 2)
            {
                enemyController.SwitchState(EnemyState.Walk);
            }
        }

        //¹¥»÷
        if (enemyController.playerDistance < enemyController.attackDistance)
        {
            if (idleTime >= 1)
            {
                enemyController.SwitchState(EnemyState.Attack);
            }
        }

        //×·»÷
        if (enemyController.playerDistance < enemyController.fllowDistance && !enemyController.isAtk)
        {
            enemyController.SwitchState(EnemyState.Walk);
        }
    }
}
