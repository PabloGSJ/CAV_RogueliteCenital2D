
public class BossStateFactory
{
    BossStateMachine _context;

    public BossStateFactory(BossStateMachine currentContext)
    {
        _context = currentContext;
    }

    public BossBaseState Phase1()
    {
        return new BossPhase1State(_context, this);
    }

    public BossBaseState Phase2()
    {
        return new BossPhase2State(_context, this);
    }

    public BossBaseState Phase3()
    {
        return new BossPhase3State(_context, this);
    }

    public BossBaseState Sleeping()
    {
        return new BossSleepingState(_context, this);
    }
}
