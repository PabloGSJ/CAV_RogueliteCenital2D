using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{

    private bool _isDed;

    private Sprite _dead0, _dead1, _dead2, _dead3;



    public PlayerDeadState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void EnterState()
    {
        _ctx.rb.bodyType = RigidbodyType2D.Static;
        _ctx.SoundController.playGameOverTuneSoundEffect();
        _isDed = false;
        _dead0 = _ctx.DeathSpriteSequence[0];
        _dead1 = _ctx.DeathSpriteSequence[1];
        _dead2 = _ctx.DeathSpriteSequence[2];
        _dead3 = _ctx.DeathSpriteSequence[3];
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        DieSequence();

        if (_isDed)
        {
            Deathloop();
        }
    }

    private void DieSequence()
    {
        
    }

    private void Deathloop()
    {
        _ctx.gameOverCanvas.SetActive(true);
        _ctx.gameOverAnim.SetBool("FadeIn", true);
        Time.timeScale = 0;

        for (;;) { }
    }
}
