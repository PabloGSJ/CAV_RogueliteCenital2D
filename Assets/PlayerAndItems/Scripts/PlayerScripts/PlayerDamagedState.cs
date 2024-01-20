using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerBaseState
{
    // CONTEXT:

    private float _timeInvulnerable;
    private float _timeFlicker;
    private float _flickerCounter;

    public PlayerDamagedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        _timeInvulnerable = _ctx.InvulnerableTime;
        _timeFlicker = _timeInvulnerable / 6;
        _flickerCounter = 2 * _timeFlicker;

        // disable collitions between the player and the enemies and enemy bullets
        _ctx.SwitchPlayerToDashLayer(true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        // move the player through the velocity variable
        _ctx.rb.velocity = _ctx.MovementVector * _ctx.Speed * Time.fixedDeltaTime;

        _timeInvulnerable -= Time.deltaTime;
        _flickerCounter -= Time.deltaTime;

        if (false)
        {
            _ctx.ShowSprite(true);
        }
        else
        {
            _ctx.ShowSprite(false);
            _flickerCounter = _timeFlicker * 2;
        }   
    }

    public override void ExitState()
    {
        _ctx.ResetDamage();
        _ctx.ShowSprite(true);
        _ctx.sr.enabled = true;

        // enable collitions between the player and the enemies and enemy bullets
        _ctx.SwitchPlayerToDashLayer(false);
    }

    public override void CheckSwitchStates()
    {
        if (_timeInvulnerable <= 0)
        {
            SwitchState(_factory.Running());
        }
    }

    public override void InitializeSubState()
    {
    }
}
