using UnityEngine;
using System.Collections;

public abstract class BaseWeapon : MonoBehaviour 
{
    // CONTEXT:

    public Camera cam;
    public Rigidbody2D rb;
    public CircleCollider2D trig;

    // Holding variables
    private PlayerStateMachine _holder = null;
    public Vector2 HandOffset;
    private Vector2 _shootingVector;

    // Shoot variables
    public GameObject BulletType;
    public int NumBullets;
    public BulletSpawnerScript BulletSpawner;
    public float ShootingForce;
    public float Cadence = 0f;  // TODO

    // MONO BEHAVIOUR FUNCTION:

    // update every game tick (very fast but irregular)
    private void Update()
    {

    }

    // update for interactions involving physics engine
    void FixedUpdate()
    {
        // follow mouse pointer while on hand
        if (_holder != null)
        {
            _shootingVector = _holder.MousePos - rb.position;
            rb.MoveRotation(Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg);
        }
    }


    // AUXILIARY FUNCTIONS:

    // Shoot a bullet
    public void Shoot()
    {
        if (Cadence == 0 && NumBullets > 0)
        {
            BulletSpawner.spawnBullet(BulletType, _shootingVector.normalized, ShootingForce);
            NumBullets--;
        }
    }

    // Sets a new holder for the weapon
    public void Pickedup(PlayerStateMachine player)
    {
        trig.enabled = false;
        this._holder = player;
        this.transform.parent = player.transform;
        this.transform.position = new Vector3(player.transform.position.x + HandOffset.x, 
                                              player.transform.position.y + HandOffset.y, 
                                              0);
    }

    // Unsets the weapon holder
    public IEnumerator Dropped()
    {
        this._holder = null;
        this.transform.parent = null;
        yield return new WaitForSeconds(1f);    // wait for the button unpress
        trig.enabled = true;
    }
}
