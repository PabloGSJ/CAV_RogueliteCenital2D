using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeleeWeapon : BaseWeapon
{
    public GameObject SlashType;
    public SlashSpawnerScript SlashSpawner;

    private float _timeElapsed;
    public float TimeBetweenAttacks;
    public float AttackRange;

    // Spawn a slash at the hands of the player
    public override void Shoot()
    {
        _timeElapsed -= Time.deltaTime;
        if (_timeElapsed <= 0)
        {
            // can attack
            SlashSpawner.spawnSlash(SlashType);


            _timeElapsed = TimeBetweenAttacks;
        }

        SlashSpawner.spawnSlash(SlashType);
    }

    protected override void DisplayUp() { }

    protected override void DisplayDown() { }
}
