using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_ctx.MovementVector != Vector2.zero)
        {
            SwitchState(_factory.Running());
        }
        else if (_ctx.IsDashing)
        {
            SwitchState(_factory.Dashing());
        }
        else if (_ctx.IsDead)
        {
            SwitchState(_factory.Dead());
        }
    }

    public override void InitializeSubState()
    {

    }
}
