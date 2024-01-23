using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSleepingState : BossBaseState
{
    public BossSleepingState(BossStateMachine currentContext, BossStateFactory bossStateFactory) : base(currentContext, bossStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsSleeping)
        {
            SwitchState(_factory.Phase1());
        }
    }

    public override void EnterState()
    {
        _ctx.IsSleeping = true;
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
}
