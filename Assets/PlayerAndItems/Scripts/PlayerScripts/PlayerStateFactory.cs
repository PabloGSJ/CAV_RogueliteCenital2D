public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Running() 
    {
        return new PlayerRunningState(_context, this);
    }

    public PlayerBaseState Dashing() 
    {
        return new PlayerDashingState(_context, this);
    }

    public PlayerBaseState Dead()
    {
        return new PlayerDeadState(_context, this);
    }
}
