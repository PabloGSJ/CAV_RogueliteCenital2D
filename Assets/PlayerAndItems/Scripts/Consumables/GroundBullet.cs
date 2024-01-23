using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBullet : BaseConsumables
{
    public override void UseConsumable(PlayerStateMachine player)
    {
        sc.playBulletsGroundSoundEffect();

        if (player.NumBullets < player.MaxPBullets)
            player.NumBullets++;
    }
}
