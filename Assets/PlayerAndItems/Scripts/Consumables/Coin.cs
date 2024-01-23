using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : BaseConsumables
{
    public override void UseConsumable(PlayerStateMachine player)
    {
        sc.playCoinSoundEffect();

        if (player.Coins < player.MaxCoins)
            player.Coins++;
    }
}
