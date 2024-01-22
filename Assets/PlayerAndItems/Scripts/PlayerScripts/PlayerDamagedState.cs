using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerBaseState
{
    // TODO: play damaged animation (flickering)

    // CONTEXT:

    private float _timeInvulnerable;

    public PlayerDamagedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        _timeInvulnerable = _ctx.InvulnerableTime;
        _ctx.sr.color = Color.gray;

        // disable collitions between the player and the enemies and enemy bullets
        _ctx.SwitchPlayerToDashLayer(true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        // move the player through the velocity variable
        _ctx.rb.velocity = _ctx.MovementVector * _ctx.Speed * Time.fixedDeltaTime;
        _timeInvulnerable -= Time.deltaTime;
    }

    public override void ExitState()
    {
        _ctx.ResetDamage();
        _ctx.sr.color = new Color(0f, 0.64869f, 1f, 1f);

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
