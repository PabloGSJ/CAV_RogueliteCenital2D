using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerBaseState
{
    // CONTEXT:

    private float _timeInvulnerable;
    private Vector2 _hitMovementVector;

    public PlayerDamagedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        //_hitMovementVector = _ctx.MovementVector;
        //_timeInvulnerable = _ctx.DashDuration;

        // disable collitions between the player and the enemies and enemy bullets
        _ctx.SwitchPlayerToDashLayer(true);
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
