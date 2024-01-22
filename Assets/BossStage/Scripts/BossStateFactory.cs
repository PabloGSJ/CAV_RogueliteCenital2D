
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
}
