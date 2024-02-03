using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{

    private bool _isDed;

    private Sprite _dead0, _dead1, _dead2, _dead3;
    private Sprite currentSprite;

    private float spriteTimer;
    private float time;


    public PlayerDeadState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchStates()
    {
        
    }

    public override void EnterState()
    {
        _ctx.rb.bodyType = RigidbodyType2D.Static;
        _ctx.mycoll.enabled = false;
        _ctx.SoundController.playGameOverTuneSoundEffect();
        _isDed = false;
        _dead0 = _ctx.DeathSpriteSequence[0];
        _dead1 = _ctx.DeathSpriteSequence[1];
        _dead2 = _ctx.DeathSpriteSequence[2];
        _dead3 = _ctx.DeathSpriteSequence[3];
        currentSprite = _dead0;
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
            Debug.Log("BUCLE");
            Deathloop();
        }
    }

    private void DieSequence()
    {
        if (currentSprite == _dead0)
        {
            currentSprite = _dead1;
            spriteTimer = Time.time;
            _ctx.sr.sprite = currentSprite;
        }
        else if (currentSprite == _dead1)
        {
            time = Time.time - spriteTimer;
            if (time >= 0.5f && spriteTimer != 0)
            {
                currentSprite = _dead2;
                spriteTimer = Time.time;
                _ctx.sr.sprite = currentSprite;
            }
        }
        else if (currentSprite == _dead2)
        {
            time = Time.time - spriteTimer;
            if (time >= 0.5f && spriteTimer != 0)
            {
                currentSprite = _dead3;
                spriteTimer = Time.time;
                _ctx.sr.sprite = currentSprite;
            }
        }
        else if (currentSprite == _dead3)
        {
            time = Time.time - spriteTimer;
            if (time >= 0.5f && spriteTimer != 0)
            {
                _isDed = true;
            }
        }
    }

    private void Deathloop()
    {
        _ctx.gameOverCanvas.SetActive(true);
        _ctx.gameOverAnim.SetBool("FadeIn", true);
        Time.timeScale = 0;
    }
}
