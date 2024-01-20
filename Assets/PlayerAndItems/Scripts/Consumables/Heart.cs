using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BaseConsumables
{
    protected override void PlaySound()
    {
        // TODO: play correct sound from sc
    }

    public override void UseConsumable(PlayerStateMachine player)
    {
        if (player.Health < player.MaxHealth)
        {
            player.Health++;
        }
    }
}
