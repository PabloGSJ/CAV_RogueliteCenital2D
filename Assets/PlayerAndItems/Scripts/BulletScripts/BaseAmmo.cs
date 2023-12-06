using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAmmo : MonoBehaviour
{
    // CONTEXT

    private GameObject empty;

    // damage variables
    public float BaseDamage = 1;
    private float _dmgMod = 0;

    // getters setters
    public float DamageModifier { set { _dmgMod = value; } }

    // FUNCIONES AUXILIARES:
    // return damage dealt by the bullet, considering damage modifiers
    public float GetDamageDealt()
    {
        return BaseDamage + _dmgMod;
    }
}
