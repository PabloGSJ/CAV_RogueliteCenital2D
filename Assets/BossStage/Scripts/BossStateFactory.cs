
public class BossStateFactory
{
    BossStateMachine _context;

    public BossStateFactory(BossStateMachine currentContext)
    {
        _context = currentContext;
    }

    public BossBaseState Idle()
    {
        return new BossIdleState(_context, this);
    }

    public BossBaseState Running() 
    {
        return new BossRunningState(_context, this);
    }
}
