public abstract class StateBase
{
    public abstract void Init(IStateMachineOwner owner);

    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void Update();

    public abstract void UnInit();
}
