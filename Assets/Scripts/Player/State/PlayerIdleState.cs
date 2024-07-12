using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    float timer;
    List<string> idleAnimations;
    public override void Init(IStateMachineOwner owner)
    {
        base.Init(owner);
        idleAnimations = new List<string>() { "Idle1", "Idle2", "Idle3" };
    }

    public override void EnterState()
    {
        base.EnterState();
        timer = 0;
        playerController.PlayAnimation("Idle");
    }


    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            int ram = Random.Range(0, idleAnimations.Count);
            playerController.PlayAnimation(idleAnimations[ram]);
            timer = 0;
        }

        if (playerController.moveIncrement != Vector3.zero)
        {
            playerController.SwitchState(PlayerState.Walk);
        }
    }
}
