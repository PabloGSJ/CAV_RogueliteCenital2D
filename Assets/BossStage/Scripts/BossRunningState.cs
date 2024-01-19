using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunningState : BossBaseState
{
    public BossRunningState(BossStateMachine currentContext, BossStateFactory bossStateFactory)
    : base(currentContext, bossStateFactory) { }

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
        if (_ctx.IsDashing)
        {
            //SwitchState(_factory.Dashing());
        }
    }

    public override void InitializeSubState()
    {

    }
}
