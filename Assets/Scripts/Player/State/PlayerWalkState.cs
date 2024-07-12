using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{

    public override void EnterState()
    {
        base.EnterState();
        playerController.PlayAnimation("Walk");
    }


    public override void Update()
    {
        base.Update();

        //×ª»»Idle
        if (playerController.moveIncrement == Vector3.zero)
        {
            playerController.SwitchState(PlayerState.Idle);
        }
        else
        {
            Vector3 forwardPointer = playerController.transform.position + playerController.moveIncrement;
            Ray ray = new Ray(forwardPointer + Vector3.up * 40, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!hit.collider.CompareTag("Lake"))
                {
                    playerController.transform.LookAt(playerController.transform.position + playerController.moveIncrement);
                    playerController.characterController.Move(playerController.transform.forward * Time.deltaTime * 4);
                    //playerController.transform.forward = Vector3.Lerp(playerController.transform.forward, playerController.moveIncrement, Time.deltaTime * 80);
                }
            }
        }
    }
}
