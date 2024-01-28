using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTake10Bullets : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        if (_active)
            return;
        _active = true;
        player.NumBullets -= 10;
        player.UpdateConsumables();
        myTextPanel.SetActive(true);
    }
}
