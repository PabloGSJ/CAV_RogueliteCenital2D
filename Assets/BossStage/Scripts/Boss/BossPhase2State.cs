using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active arsenal:
// - Cannon
// - Hands
public class BossPhase2State : BossBaseState
{
    // Active arsenal
    private const int Radius    = 0;
    private const int Cone      = 1;
    private const int Straight  = 2;
    private const int Giant     = 3;
    // Cannon
    private CannonController _cc;
    private float _cannonCounter;
    // Hands
    private HandsController _hc;
    private float _handsCounter;


    public BossPhase2State(BossStateMachine currentContext, BossStateFactory bossStateFactory) 
    : base(currentContext, bossStateFactory)
    {
    }

    public override void EnterState()
    {
        _cc = _ctx.cc;
        _hc = _ctx.hc;
        _cannonCounter = 0;
        _handsCounter = 0;

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
            // Hands
            _ctx.HandsCooldown = 1;
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        // Try attack with the cannon
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

        // Try attack with the hands
        if (_handsCounter <= 0)
        {
            _handsCounter = _ctx.HandsCooldown;
            // Select an random attack to perform
            switch (Random.Range(0, 4))
            {
                case Radius:
                    _hc.FireInAllDirections();
                    break;

                case Cone:
                    _hc.FireConeTowardsDirection(_ctx.Player.transform.position);
                    break;

                case Straight:
                    _hc.FireStraightAtDirection(_ctx.Player.transform.position);
                    break;
                case Giant:
                    _hc.FireGiantBulletAtDirection(_ctx.Player.transform.position);
                    break;
                default:
                    break;
            }
        }
        _cannonCounter -= Time.deltaTime;
        _handsCounter -= Time.deltaTime;
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Health <= _ctx.MaxHealth / 3)
        {
            SwitchState(_factory.Phase3());
        }
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
