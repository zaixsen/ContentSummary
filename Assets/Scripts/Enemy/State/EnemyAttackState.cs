using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{

    public override void EnterState()
    {
        base.EnterState();

        enemyController.PlayAnimation("Attack");
    }


    public override void Update()
    {
        base.Update();

        Vector3 fllowDir = (enemyController.player.transform.position - enemyController.transform.position).normalized;
        enemyController.transform.forward = Vector3.Lerp(enemyController.transform.forward, fllowDir, Time.deltaTime * 8);

        if (enemyController.animInfo.normalizedTime >= 1f && !enemyController.animator.IsInTransition(0))
        {
            enemyController.SwitchState(EnemyState.Idle);
        }
    }


}
