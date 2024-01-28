using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKnucledusters : BaseMeleeWeapon
{
    protected override void PlayMySoundEffect()
    {
        sc.playPunchSoundEffect();
    }
}
