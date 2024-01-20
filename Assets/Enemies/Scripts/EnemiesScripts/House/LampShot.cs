using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampShot : MonoBehaviour
{
    public GameObject prefab;
    private Transform player;

    public Room enemyRoom;
    private RoomController rc;

    public float cd;
    public float v;
    private float timer;
    private float spriteTimer;
    private float time;

    //Sprites
    public Sprite lamp_shot0;
    public Sprite lamp_shot1;
    public Sprite lamp_shot2;
    public Sprite currentSprite;

    public void Start()
    {
        currentSprite = lamp_shot0;
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
            if (currentSprite == lamp_shot0 && timer <= cd / 4)
            {
                currentSprite = lamp_shot1;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
            else if (currentSprite == lamp_shot2)
            {
                time = Time.time - spriteTimer;
                if (time >= 0.5f && spriteTimer != 0)
                {
                    spriteTimer = 0;
                    currentSprite = lamp_shot1;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }
            if (timer <= 0f)
            {
                spriteTimer = Time.time;
                currentSprite = lamp_shot2;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                Shoot();
                timer = cd;
            }
        }
        else
        {
            spriteTimer = 0;
            currentSprite = lamp_shot0;
            this.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }
    }

    private void Shoot()
    {
        Vector2 dir = (player.position - transform.position).normalized;


        for (int i = 0; i < 2; i++)
        {
                
            GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = dir * v;

        }
        
    }
}

