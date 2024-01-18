using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private float healthPoints = 3.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:     // PlayerBullets layer
                // The enemy is hit by a bullet
                healthPoints -= collision.gameObject.GetComponent<BaseAmmo>().GetDamageDealt();
                if (healthPoints <= 0)
                {
                    // The enemy dies
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
