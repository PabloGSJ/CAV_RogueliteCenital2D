using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseConsumables
{ 
    protected override void PlaySound()
    {
        sc.playHealthPotionSoundEffect();
    }

    public override void UseConsumable(PlayerStateMachine player)
    {
        Debug.Log("consumed potion");
        player.Health = player.MaxHealth;
    }
}
