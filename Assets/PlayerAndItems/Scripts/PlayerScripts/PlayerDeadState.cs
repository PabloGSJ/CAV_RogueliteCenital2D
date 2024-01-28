using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void EnterState()
    {
        _ctx.rb.bodyType = RigidbodyType2D.Static;
        _ctx.a.SetBool(_ctx.AnimIsDead, true);
        _ctx.SoundController.playGameOverTuneSoundEffect();
        _ctx.gameOverCanvas.SetActive(true);
        _ctx.gameOverAnim.SetBool("FadeIn", true);
        Time.timeScale = 0;
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
       
    }
}
