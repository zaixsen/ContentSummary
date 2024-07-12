public class PlayerStateBase : StateBase
{
    protected PlayerController playerController;

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void Init(IStateMachineOwner owner)
    {
        playerController = owner as PlayerController;
    }

    public override void UnInit()
    {

    }

    public override void Update()
    {

    }
}
