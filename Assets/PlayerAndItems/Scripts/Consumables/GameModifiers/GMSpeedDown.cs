using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSpeedDown : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        if (_active)
            return;
        _active = true;
        player.Speed = player.Speed / 2;
        myTextPanel.SetActive(true);
    }
}
