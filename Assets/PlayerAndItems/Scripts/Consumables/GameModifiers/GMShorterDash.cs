using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMShorterDash : BaseGM
{
    public override void UseGM(PlayerStateMachine player)
    {
        player.DashDuration = 0.05f;
        myTextPanel.SetActive(true);
    }
}
