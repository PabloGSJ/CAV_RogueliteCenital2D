using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMNumEnemiesUp : BaseGM
{
    protected override void ApplyEffect()
    {
        gmm.ChangeNumEnemiesMod(10);
    }
}
