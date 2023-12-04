using UnityEngine;

public class BulletSpawnerScript : MonoBehaviour
{
    private GameObject empty;

    private void Awake()
    {
        // preserve the bullets absolute proportions
        empty = GameObject.FindGameObjectWithTag("Empty");
        if (empty != null)
            Debug.Log(empty);
    }

    public void spawnBullet(GameObject bullet, Vector2 shootingVector, float bulletSpeed, float dmgMod, Quaternion weaponRotation)
    {
        GameObject go = Instantiate(bullet,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    weaponRotation,
                                    empty.transform) as GameObject;
        go.transform.parent = empty.transform;
        go.GetComponent<Rigidbody2D>().velocity = shootingVector * bulletSpeed;
        go.GetComponent<BaseBullet>().DamageModifier = dmgMod;
    }
}
