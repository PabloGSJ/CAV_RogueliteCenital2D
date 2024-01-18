public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Running() 
    {
        return new PlayerRunningState(_context, this);
    }

    public PlayerBaseState Dashing() 
    {
        return new PlayerDashingState(_context, this);
    }
}
