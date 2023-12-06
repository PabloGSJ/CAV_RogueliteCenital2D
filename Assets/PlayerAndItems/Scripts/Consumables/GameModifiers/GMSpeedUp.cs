using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSpeedUP : BaseGM
{
    protected override void ApplyEffect()
    {
        gmm.ChangeSpeedMod(100);
    }
}