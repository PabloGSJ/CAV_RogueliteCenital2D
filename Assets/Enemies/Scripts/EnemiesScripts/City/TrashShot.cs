using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashShot : MonoBehaviour
{
    public GameObject prefab;

    public Room enemyRoom;
    private RoomController rc;

    public float cd;
    public float v;
    private float timer;
    private int shoots;

    //Sprites
    private float spriteTimer;
    private float time;

    public Sprite trash_shot0;
    public Sprite trash_shot1;
    public Sprite trash_shot2;
    public Sprite trash_shot3;
    public Sprite currentSprite;


    public void Start()
    {
        spriteTimer = 0;
        currentSprite = trash_shot0;
        enemyRoom = gameObject.transform.parent.gameObject.GetComponent<Room>();
        rc = FindObjectOfType<RoomController>();
        timer = cd;
        shoots = 0;
    }
    public void Update()
    {

        if (rc.CurrentRoom == enemyRoom)
        {
            timer -= Time.deltaTime;
            if (currentSprite == trash_shot0 && timer <= cd / 4)
            {
                currentSprite = trash_shot1;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
            else if (currentSprite == trash_shot3)
            {
                time = Time.time - spriteTimer;
                if (time >= 0.5f && spriteTimer != 0)
                {
                    spriteTimer = Time.time;
                    currentSprite = trash_shot2;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }
            else if (currentSprite == trash_shot1)
            {
                time = Time.time - spriteTimer;
                if (time >= 0.5f && spriteTimer != 0)
                {
                    spriteTimer = 0;
                    currentSprite = trash_shot2;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }

            if (timer <= 0f)
            {
                spriteTimer = Time.time;
                currentSprite = trash_shot3;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                shoots += 1;
                Shoot4D();
                timer = cd;
            }
        }
        else
        {
            spriteTimer = 0;
            currentSprite = trash_shot0;
            this.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }

    }

    private void Shoot4D()
    {
        Vector2 up;
        Vector2 down;
        Vector2 left;
        Vector2 right;

        if (shoots < 2)
        {
            up = Vector2.up.normalized;
            down = Vector2.down.normalized;
            right = Vector2.right.normalized;
            left = Vector2.left.normalized;
        }
        else
        {
            up = new Vector2(1, 1);
            down = new Vector2(-1, -1);
            right = new Vector2(-1, 1);
            left = new Vector2(1, -1);
            shoots = 0;
        }

        GameObject projectileUp = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileUp.GetComponent<Rigidbody2D>().velocity = up * v;

        GameObject projectileDown = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileDown.GetComponent<Rigidbody2D>().velocity = down * v;

        GameObject projectileRight = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileRight.GetComponent<Rigidbody2D>().velocity = right * v;

        GameObject projectileLeft = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileLeft.GetComponent<Rigidbody2D>().velocity = left * v;
    }
}
