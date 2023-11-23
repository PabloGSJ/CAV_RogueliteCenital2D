using UnityEngine;

public class BulletSpawnerScript : MonoBehaviour
{
    public void spawnBullet(GameObject bullet, Vector2 shootingVector, float bulletSpeed)
    {
        GameObject go = Instantiate(bullet,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    Quaternion.identity,
                                    gameObject.transform) as GameObject;

        go.GetComponent<Rigidbody2D>().velocity = shootingVector * bulletSpeed;
        Debug.Log("velocity = " + shootingVector * bulletSpeed);
        Debug.Log("shootingVector = " + shootingVector);
        Debug.Log("bulletSpeed = " + bulletSpeed);
    }
}
