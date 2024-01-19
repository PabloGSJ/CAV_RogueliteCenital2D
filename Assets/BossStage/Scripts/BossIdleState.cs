using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    public BossIdleState(BossStateMachine currentContext, BossStateFactory bossStateFactory)
    : base(currentContext, bossStateFactory) { }

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
    }

    public override void InitializeSubState()
    {

    }
}
