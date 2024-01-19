using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeFuegoDamager : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerStateMachine>())
        {
            PlayerStateMachine playerHealth = other.gameObject.GetComponent<PlayerStateMachine>();
            playerHealth.TakeDamage(damage);
        }
    }
}