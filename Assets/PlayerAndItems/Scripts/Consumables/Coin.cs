using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : BaseConsumables
{
    public override void UseConsumable(PlayerStateMachine player)
    {
        if (player.Coins < player.MaxCoins)
            player.Coins++;
    }
}
