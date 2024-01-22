using UnityEngine;

public abstract class BaseBullet : BaseAmmo
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)   // Enemy layer
        {
            Destroy(gameObject);
        }
    }
}
