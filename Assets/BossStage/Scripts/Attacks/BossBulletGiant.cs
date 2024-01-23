using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletGiant : BaseBullet
{
    public Rigidbody2D rb;
    private SoundControllerScript sc;

    // Invulnerability
    private const int GhostBulletLayer = 20;
    private const int EnemyBulletsLayer = 12;
    public float GhostTime = 0.1f;
    private float _iCounter;

    public GameObject Bullet;
    public int Speed;

    public float ShootStep;
    public float AngleStep;
    private float _counter;
    private float _angle;

    public float SpinningSpeed;


    private void Awake()
    {
        sc = GameObject.Find("SoundControl").GetComponent<SoundControllerScript>();

        _counter = 0;
        _angle = 0;
        _iCounter = GhostTime;
    }

    private void FixedUpdate()
    {
        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
            this.gameObject.layer = GhostBulletLayer;
        }
        else
        {
            this.gameObject.layer = EnemyBulletsLayer;
        }

        rb.rotation += SpinningSpeed;      // spin the bullet

        // Shoot bullets in spinning pattern
        if (_counter <= 0)
        {
            _counter = ShootStep;
            
            Vector2 shootingVector = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle));
            Shoot(shootingVector);
            _angle += AngleStep;
        }
        _counter -= Time.deltaTime;

        if (_iCounter > 0)
        {
            _iCounter -= Time.deltaTime;
            this.gameObject.layer = GhostBulletLayer;
        }
        else
        {
            this.gameObject.layer = EnemyBulletsLayer;
        }
    }


    public void Shoot(Vector2 shootingVector)
    {
        sc.playEnergyProjectileShotSoundEffect();

        GameObject go = Instantiate(Bullet,
                                    this.transform.position,
                                    this.transform.rotation) as GameObject;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        //rb.MoveRotation(Mathf.Atan2(shootingVector.y, shootingVector.x) * Mathf.Rad2Deg);
        rb.velocity = shootingVector * Speed;
    }
}
