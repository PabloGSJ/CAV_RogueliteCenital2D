using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMDmgUp : BaseGM
{
    protected override void ApplyEffect()
    {
        gmm.ChangeDmgMod(10);
    }
}
