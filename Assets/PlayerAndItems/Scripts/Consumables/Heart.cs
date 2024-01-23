using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BaseConsumables
{
    public override void UseConsumable(PlayerStateMachine player)
    {
        sc.playHeartSoundEffect();

        if (player.Health < player.MaxHealth)
        {
            player.Health += 2;
        }
    }
}
