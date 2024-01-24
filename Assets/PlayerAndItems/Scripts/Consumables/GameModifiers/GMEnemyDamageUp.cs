using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMEnemyDamageUp : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        if (_active)
            return;
        _active = true;
        player.DamageModifier = 2;
        myTextPanel.SetActive(true);

    }
}
