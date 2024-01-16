using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRound : BaseConsumables
{
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
