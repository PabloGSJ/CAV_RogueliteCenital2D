using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : BaseRangedWeapon
{
    public int NumBuckshots;
    public float BulletSpreadFactor;      // [20, 10]
    public float BulletSpeedFactor;

    public override void Shoot(float dmgMod)
    {
        if ((TryShoot() || _holder.TryBorrowBullet()) && _cadenceCounter <= 0)
        {
            if (NumBullets > 0)
            {
                NumBullets--;
                ui.DisplayNewWNBullets(NumBullets);
            }
            else
            {
                _holder.BorrowBullet();
            }

            // shoot the bullets in an arch
            // code source: https://www.reddit.com/r/Unity2D/comments/76dyvl/how_do_you_do_a_shotgun_spread_in_a_topdown/
            float spreadMod, forceMod, shootingAngle;
            Quaternion rotation;
            Vector2 shootingDirection;

            for (int i = 0; i < NumBuckshots; i++)
            {
                // Randomize
                spreadMod = Random.Range(-BulletSpreadFactor, BulletSpreadFactor);
                forceMod = Random.Range(0, BulletSpeedFactor);

                // Calculate the direction of the shot (angle)
                shootingAngle = spreadMod + (Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg);
                rotation = Quaternion.Euler(0, 0, shootingAngle);   // bullet sprite rotation
                // Calculate the direction of the shot (vector.normalized)
                shootingDirection = new Vector2(
                    Mathf.Cos(shootingAngle * Mathf.Deg2Rad),
                    Mathf.Sin(shootingAngle * Mathf.Deg2Rad)).normalized;

                BulletSpawner.spawnBullet(BulletType, shootingDirection, ShootingForce + forceMod, dmgMod, rotation);
            }

            _cadenceCounter = Cadence;
            PlayMySoundEffect();
        }
    }
}
