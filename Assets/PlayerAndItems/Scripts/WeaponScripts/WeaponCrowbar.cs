using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrowbar : BaseMeleeWeapon
{
    // Because of the special properties of the Crowbar weapon, the sprite is a separate child GameObject
    public SpriteRenderer realsr;

    

    protected override void FollowPointer()
    {
        // The crowbar sprite is at a 45 degree angle.
        // To make the tip of the crowbar follow the pointer, subtract 45 degrees from the current angle
        rb.MoveRotation(Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg - 45);
    }

    private void Update()
    {
        realsr.sortingLayerID = sr.sortingLayerID;
        realsr.sortingOrder = sr.sortingOrder;
    }

    // This is a CHAPUZA
    // Executed after picking up the weapon
    protected override void DisplayUp()
    {
    }

    // Executed when dropping the weapon
    protected override void DisplayDown()
    {
    }

    protected override void PlayMySoundEffect()
    {
        sc.playSwordSwingSoundEffect();
    }
}
