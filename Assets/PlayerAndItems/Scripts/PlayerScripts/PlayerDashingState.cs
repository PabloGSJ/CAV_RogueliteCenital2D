using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    // CONTEXT

    private float _dashActiveCounter;
    private Vector2 _dashMovVector;
    

    public PlayerDashingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.SoundController.playDashSoundEffect();

        _ctx.a.SetBool(_ctx.AnimIsDashing, true);

        _dashMovVector = _ctx.MovementVector;
        _dashActiveCounter = _ctx.DashDuration;

        // disable collitions between the player and the enemies and enemy bullets
        _ctx.SwitchPlayerToDashLayer(true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        _dashActiveCounter -= Time.deltaTime;
        _ctx.rb.velocity = _dashMovVector * _ctx.DashSpeed * Time.fixedDeltaTime;
    }

    public override void ExitState()
    {
        _ctx.a.SetBool(_ctx.AnimIsDashing, false);

        _ctx.ResetDash();

        // enable collitions between the player and the enemies and enemy bullets
        _ctx.SwitchPlayerToDashLayer(false);
    }

    public override void CheckSwitchStates()
    {
        if (_dashActiveCounter <= 0)
        {
            SwitchState(_factory.Running());
        }
    }

    public override void InitializeSubState()
    {

    }
}
