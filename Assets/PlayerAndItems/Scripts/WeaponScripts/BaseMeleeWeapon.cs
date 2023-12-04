using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeleeWeapon : BaseWeapon
{
    public GameObject SlashType;
    public SlashSpawnerScript SlashSpawner;

    // Spawn a slash at the hands of the player
    public override void Shoot()
    {
        SlashSpawner.spawnSlash(SlashType);
    }

    protected override void DisplayUp() { }

    protected override void DisplayDown() { }
}
