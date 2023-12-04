using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAmmo : MonoBehaviour
{
    public float BaseDamage = 1;
    private float _dmgMod = 0;

    // getters setters
    public float DamageModifier { set { _dmgMod = value; } }

    public float GetDamageDealt()
    {
        return BaseDamage + _dmgMod;
    }
}
