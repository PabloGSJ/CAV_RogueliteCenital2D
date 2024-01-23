using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BaseBullet
{
    private const int GhostBulletLayer = 20;
    private const int EnemyBulletsLayer = 12;
    public float GhostTime = 0.1f;
    private float _counter;

    private void Awake()
    {
        _counter = GhostTime;
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
    }


}
