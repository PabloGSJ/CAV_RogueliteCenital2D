using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTake10Bullets : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        player.NumBullets -= 10;
        if (player.NumBullets < 0)
            player.NumBullets = 0;
        player.UpdateConsumables();
        myTextPanel.SetActive(true);
    }
}
