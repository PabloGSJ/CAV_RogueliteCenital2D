using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2State : BossBaseState
{
    public BossPhase2State(BossStateMachine currentContext, BossStateFactory bossStateFactory) 
    : base(currentContext, bossStateFactory)
    {
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.Health < _ctx.MaxHealth / 3)
        {
            // Advance phase
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
