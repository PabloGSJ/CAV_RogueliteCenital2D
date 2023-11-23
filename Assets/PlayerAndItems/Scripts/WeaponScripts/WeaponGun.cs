using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : BaseWeapon
{
    private void Awake()
    {
        // HandOffset = new Vector2(0.5f, 0.1f);
        // NumBullets = 10;
        Debug.Log(BulletType);
    }
}
