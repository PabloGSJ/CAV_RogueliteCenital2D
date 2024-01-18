using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMEnemiesDropBulletsFalse : BaseGM
{
    protected override void ApplyEffect()
    {
        gmm.ChangeEnemiesDropBullets(false);
    }
}
