using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>()) {
            EnemyHealth enemyHealt = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealt.TakeDamage(damageAmount);
        }
    }
}
