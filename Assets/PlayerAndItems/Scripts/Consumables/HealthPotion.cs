using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseConsumables
{ 
    public override void UseConsumable(PlayerStateMachine player)
    {
        sc.playHealthPotionSoundEffect();

        Debug.Log("consumed potion");
        player.Health = player.MaxHealth;
    }
}
