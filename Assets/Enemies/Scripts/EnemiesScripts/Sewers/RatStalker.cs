using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RatStalker : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 230f;

    private RoomController rc;
    public Room enemyRoom;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Transform player;

    //Sprites
    private float spriteTimer;
    private float time;

    public Sprite rat_follow0;
    public Sprite rat_follow1;
    public Sprite rat_follow2;
    public Sprite currentSprite;

    private void Awake()
    {
        spriteTimer = 0;
        currentSprite = rat_follow0;
        rb = GetComponent<Rigidbody2D>();
        enemyRoom = gameObject.transform.parent.gameObject.GetComponent<Room>();
        rc = FindObjectOfType<RoomController>();
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {

        if (rc.CurrentRoom == enemyRoom && player != null)
        {
            if (spriteTimer == 0)
            {
                spriteTimer = Time.time;
            }

            if (currentSprite == rat_follow0)
            {
                time = Time.time - spriteTimer;
                if (time >= 0.5f)
                {
                    currentSprite = rat_follow1;
                    spriteTimer = Time.time;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }
            else if (currentSprite == rat_follow1)
            {
                time = Time.time - spriteTimer;
                if (time >= 1f && spriteTimer != 0)
                {
                    currentSprite = rat_follow2;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }

            moveDirection = player.position;

            Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 finalDirection = (moveDirection - position).normalized;

            rb.velocity = finalDirection * (moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            spriteTimer = 0;
            currentSprite = rat_follow0;
            this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            rb.velocity = new Vector2(0, 0);
        }
    }

}

