using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRound : BaseConsumables
{
    protected override void PlaySound()
    {
        // TODO: play correct sound from sc
    }

    // number of bullets
    public int NumBullets = 10;

    public override void UseConsumable(PlayerStateMachine player)
    { 
        player.NumBullets += NumBullets;

        if (player.NumBullets > player.MaxPBullets)
        {
            player.NumBullets = player.MaxPBullets;
        }
    }
}
