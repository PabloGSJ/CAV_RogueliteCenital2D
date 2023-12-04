using UnityEngine;

public abstract class BaseBullet : BaseAmmo
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
