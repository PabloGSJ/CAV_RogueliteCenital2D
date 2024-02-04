using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMMaxHealthHalved : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        player.MaxHealth = player.MaxHealth / 2;
        player.UpdateMaxHealth();
        player.TakeDamage(0);
        myTextPanel.SetActive(true);
    }
}
