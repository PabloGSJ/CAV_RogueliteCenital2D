using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSpeedDown : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        player.Speed = player.Speed / 2;
        myTextPanel.SetActive(true);
    }
}
