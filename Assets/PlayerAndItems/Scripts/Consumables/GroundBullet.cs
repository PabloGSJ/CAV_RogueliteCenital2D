using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBullet : BaseConsumables
{
    protected override void UseConsumable(PlayerStateMachine player)
    {
        player.NumBullets++;
    }
}
