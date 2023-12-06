using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRangedWeapon : BaseWeapon
{
    // CONTEXT: 

    // Shoot variales
    public GameObject BulletType;
    public BulletSpawnerScript BulletSpawner;
    public int NumBullets;
    public float ShootingForce;

    // Spawn a bullet at the tip of the weapon
    public override void Shoot(float dmgMod)
    {
        if (TryShoot())
        {
            BulletSpawner.spawnBullet(BulletType, _shootingVector.normalized, ShootingForce, dmgMod, this.transform.rotation);
        }
    }

    protected bool TryShoot()
    {
        if (NumBullets > 0)
        {
            NumBullets--;
            ui.DisplayNewWNBullets(NumBullets);
            return true;
        }

        return _holder.BorrowBullet();
    }

    protected override void DisplayUp()
    {
        ui.DisplayNewWNBullets(NumBullets);
        ui.EnableWeaponNBullets(true);
    }

    protected override void DisplayDown()
    {
        ui.EnableWeaponNBullets(false);
    }
}
