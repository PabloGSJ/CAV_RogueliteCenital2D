using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeleeWeapon : BaseWeapon
{
    public GameObject SlashType;
    public SlashSpawnerScript SlashSpawner;

    protected override void FollowPointer()
    {
        rb.MoveRotation(Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg);
    }

    // Spawn a slash at the hands of the player
    public override void Shoot(float dmgMod)
    { 
        if (_cadenceCounter <= 0)
        {
            SlashSpawner.spawnSlash(SlashType, dmgMod, this.transform.rotation);
            _cadenceCounter = Cadence;
            PlayMySoundEffect();
        }
    }

    protected override void PlayMySoundEffect()
    {
        
    }

    protected override void DisplayUp() { }

    protected override void DisplayDown() { }
}
