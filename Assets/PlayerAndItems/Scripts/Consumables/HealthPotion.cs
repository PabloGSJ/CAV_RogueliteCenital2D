using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseConsumables
{
    protected override void UseConsumable(PlayerStateMachine player)
    {
        player.Health = player.MaxHealth;
    }
}
