
public abstract class BossBaseState
{
    protected BossStateMachine _ctx;
    protected BossStateFactory _factory;
    public BossBaseState(BossStateMachine currentContext, BossStateFactory bossStateFactory)
    {
        _ctx = currentContext;
        _factory = bossStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    void UpdateStates() { }

    protected void SwitchState(BossBaseState newState) 
    {
        ExitState();
        newState.EnterState();

        _ctx.CurrentState = newState;
    }

    protected void SetSuperState() { }

    protected void SetSubState() { }
}
