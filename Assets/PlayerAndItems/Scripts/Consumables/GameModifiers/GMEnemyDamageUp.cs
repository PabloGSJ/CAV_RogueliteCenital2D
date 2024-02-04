using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMEnemyDamageUp : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        player.DamageModifier = 2;
        myTextPanel.SetActive(true);
    }
}
