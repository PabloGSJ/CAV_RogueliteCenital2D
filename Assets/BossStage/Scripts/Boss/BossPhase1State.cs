using UnityEngine;

// Active arsenal:
// - Cannon

public class BossPhase1State : BossBaseState
{
    private float _cannonCounter;

    private const int Radius    = 0;
    private const int Cone      = 1;
    private const int Straight  = 2;

    // Active arsenal
    private CannonController _cc;

    public BossPhase1State(BossStateMachine currentContext, BossStateFactory bossStateFactory) 
    : base(currentContext, bossStateFactory)
    {
    }

    public override void EnterState()
    {
        _cc = _ctx.cc;
        _cannonCounter = 0;

        if (_ctx.LoadArsenalStatic)
        {
            // Statically load information into active arsenal:
            // Cannon
            _ctx.CannonCooldown = 1;
            _cc.Step = 0.1f;
            _cc.RadialBullets = 13;
            _cc.RadialRepeats = 3;
            _cc.ConeBullets = 3;
            _cc.ConeSpread = 15;
            _cc.ConeRepeats = 5;
            _cc.StraightRepeats = 5;
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        
        if (_cannonCounter <= 0)
        {
            _cannonCounter = _ctx.CannonCooldown;

            // Select an random attack to perform
            switch (Random.Range(0, 3))
            {
                case Radius:
                    _cc.FireInAllDirections();
                    break;

                case Cone:
                    _cc.FireConeTowardsDirection(_ctx.Player.transform.position);
                    break;

                case Straight:
                    _cc.FireStraightAtDirection(_ctx.Player.transform.position);
                    break;
                default:
                    break;
            }
        }
        _cannonCounter -= Time.deltaTime;
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Health <= (_ctx.MaxHealth / 3) * 2)
        {
            // Advance phase
            SwitchState(_factory.Phase2());
        }
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
