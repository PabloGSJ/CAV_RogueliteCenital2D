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
    public override void Shoot()
    {
        if (NumBullets > 0)
        {
            BulletSpawner.spawnBullet(BulletType, _shootingVector.normalized, ShootingForce);
            NumBullets--;
            ui.DisplayNewWNBullets(NumBullets);

        }
        else if (_holder.BorrowBullet())
        {
            BulletSpawner.spawnBullet(BulletType, _shootingVector.normalized, ShootingForce);
        }
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
