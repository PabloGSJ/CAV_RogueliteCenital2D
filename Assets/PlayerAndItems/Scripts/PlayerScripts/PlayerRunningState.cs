using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public PlayerRunningState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        // move the player through the velocity variable
        _ctx.rb.velocity = _ctx.MovementVector * _ctx.Speed * Time.fixedDeltaTime;
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.MovementVector == Vector2.zero)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.IsDashing)
        {
            SwitchState(_factory.Dashing());
        }
        else if (_ctx.IsDamaged)
        {
            SwitchState(_factory.Damaged());
        }
    }

    public override void InitializeSubState()
    {

    }
}
