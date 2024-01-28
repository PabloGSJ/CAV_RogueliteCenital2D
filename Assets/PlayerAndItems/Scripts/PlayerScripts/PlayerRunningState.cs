using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public PlayerRunningState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _ctx.a.SetBool(_ctx.AnimIsMoving, true);
        _ctx.SoundController.playRunSoundEffect();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        // move the player through the velocity variable
        _ctx.rb.velocity = _ctx.MovementVector * _ctx.Speed * Time.fixedDeltaTime;

        if (_ctx.MovementVector.x < 0)
        {
            // facing left
            _ctx.pr.transform.localScale = new Vector3(_ctx.gameObject.transform.localScale.x * -1,
                                                       _ctx.gameObject.transform.localScale.y,
                                                       _ctx.gameObject.transform.localScale.z);
        }
        else
        {
            // facing right
            _ctx.pr.transform.localScale = new Vector3(_ctx.gameObject.transform.localScale.x * 1,
                                                       _ctx.gameObject.transform.localScale.y,
                                                       _ctx.gameObject.transform.localScale.z);
        }
    }

    public override void ExitState()
    {
        _ctx.a.SetBool(_ctx.AnimIsMoving, false);
        _ctx.SoundController.stopRunSoundEffect();
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
        else if (_ctx.IsDead)
        {
            SwitchState(_factory.Dead());
        }
    }

    public override void InitializeSubState()
    {

    }
}
