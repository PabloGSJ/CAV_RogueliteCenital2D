using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        DetectDeath();
        Debug.Log(currentHealth);
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
            gameObject.transform.parent.gameObject.GetComponentInChildren<FightController>().enemyDeath(gameObject.GetInstanceID());
        }
    }
}
