public abstract class EnemyBaseState
{
    private protected EnemyStateMachine ctx;
    private protected EnemyStateFactory factory;

    public EnemyBaseState(EnemyStateMachine context, EnemyStateFactory enemyStateFactory)
    {
        ctx = context;
        factory = enemyStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    void UpdateStates() { }
    protected void SwitchState(EnemyBaseState newState) 
    {
        // current state exits state
        ExitState();

        // new sate enters state
        newState.EnterState();

        //switch current state of context
        ctx.CurrentState = newState;
    }
    protected void SetSuperState() { }
    protected void SetSubState() { }
}
