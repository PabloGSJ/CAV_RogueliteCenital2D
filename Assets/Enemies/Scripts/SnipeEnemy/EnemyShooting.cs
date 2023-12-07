using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject prefab;
    private Transform player;

    public Room enemyRoom;
    private RoomController rc;

    public float cd;
    public float v;
    private float timer;

    public void Start()
    {
        rc = FindObjectOfType<RoomController>();
        enemyRoom = gameObject.transform.parent.gameObject.GetComponent<Room>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = cd;
    }

    public void Update()
    {

        if (rc.CurrentRoom == enemyRoom)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Shoot();
                timer = cd;
            }
        }
    }

    private void Shoot()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(prefab,transform.position,Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = dir * v;
    }
}
