using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseConsumables
{ 
    protected override void PlaySound()
    {
        // TODO: play correct sound from sc
    }

    public override void UseConsumable(PlayerStateMachine player)
    {
        Debug.Log("consumed potion");
        player.Health = player.MaxHealth;
    }
}
