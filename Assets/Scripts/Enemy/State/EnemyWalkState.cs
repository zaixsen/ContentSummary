using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWalkState : EnemyStateBase
{
    Transform[] paths;
    int pathIndex;
    public override void Init(IStateMachineOwner owner)
    {
        base.Init(owner);
        pathIndex = 1;   //从一开始 去除路径点自身位置
        paths = enemyController.pathParent.GetComponentsInChildren<Transform>();
    }
    public override void EnterState()
    {
        base.EnterState();
        enemyController.PlayAnimation("Walk");
    }

    public override void Update()
    {
        base.Update();

        //走路点 巡逻路径
        if (enemyController.playerDistance > enemyController.fllowDistance)
        {
            Vector3 moveDir = (paths[pathIndex].position - enemyController.transform.position).normalized;
            enemyController.transform.forward = Vector3.Lerp(enemyController.transform.forward, moveDir, Time.deltaTime * 8);
            enemyController.characterController.Move(moveDir * Time.deltaTime);
            float dis = Vector3.Distance(enemyController.transform.position, paths[pathIndex].position);
            if (dis <= 1)
            {
                pathIndex++;

                enemyController.SwitchState(EnemyState.Idle);

                if (pathIndex == paths.Length)
                {
                    pathIndex = 1;
                }
            }
        }

        //追击玩家
        if (enemyController.playerDistance < enemyController.fllowDistance)
        {
            Vector3 fllowDir = (enemyController.player.transform.position - enemyController.transform.position).normalized;

            enemyController.transform.forward = Vector3.Lerp(enemyController.transform.forward, fllowDir, Time.deltaTime * 8);
            enemyController.characterController.Move(fllowDir * Time.deltaTime);
        }

        if (enemyController.isAtk)
        {
            enemyController.SwitchState(EnemyState.Attack);
        }
    }
}
