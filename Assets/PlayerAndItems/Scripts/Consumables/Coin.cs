using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : BaseConsumables
{
    protected override void UseConsumable(PlayerStateMachine player)
    {
        player.Coins++;
    }
}
