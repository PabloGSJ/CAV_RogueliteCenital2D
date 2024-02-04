using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShot : MonoBehaviour
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

    public Sprite mushroom_shot0;
    public Sprite mushroom_shot1;
    public Sprite mushroom_shot2;
    public Sprite mushroom_shot3;
    public Sprite currentSprite;


    public void Start()
    {
        spriteTimer = 0;
        currentSprite = mushroom_shot0;
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
            if (currentSprite == mushroom_shot0 && timer <= cd / 4)
            {
                currentSprite = mushroom_shot1;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
            else if (currentSprite == mushroom_shot3)
            {
                time = Time.time - spriteTimer;
                if (time >= 0.5f && spriteTimer != 0)
                {
                    spriteTimer = Time.time;
                    currentSprite = mushroom_shot2;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }
            else if (currentSprite == mushroom_shot1)
            {
                time = Time.time - spriteTimer;
                if (time >= 0.5f && spriteTimer != 0)
                {
                    spriteTimer = 0;
                    currentSprite = mushroom_shot2;
                    this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                }
            }

            if (timer <= 0f)
            {
                spriteTimer = Time.time;
                currentSprite = mushroom_shot3;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
                shoots += 1;
                Shoot4D();
                timer = cd;
            }
        }
        else
        {
            spriteTimer = 0;
            currentSprite = mushroom_shot0;
            this.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }

    }

    private void Shoot4D()
    {
        Vector2 up;
        Vector2 down;
        Vector2 left;
        Vector2 right;

        GameObject projectileUp;
        GameObject projectileDown;
        GameObject projectileRight;
        GameObject projectileLeft;

        Vector2 upD;
        Vector2 downD;
        Vector2 leftD;
        Vector2 rightD;

        GameObject projectileUpD;
        GameObject projectileDownD;
        GameObject projectileRightD;
        GameObject projectileLeftD;

        up = Vector2.up.normalized;
        down = Vector2.down.normalized;
        right = Vector2.right.normalized;
        left = Vector2.left.normalized;

        if (shoots < 2)
        {

            projectileUp = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileUp.GetComponent<Rigidbody2D>().velocity = up * v;

            projectileDown = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileDown.GetComponent<Rigidbody2D>().velocity = down * v;

            projectileRight = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileRight.GetComponent<Rigidbody2D>().velocity = right * v;

            projectileLeft = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileLeft.GetComponent<Rigidbody2D>().velocity = left * v;
        }
        else
        {
            upD = new Vector2(1, 1);
            downD = new Vector2(-1, -1);
            rightD = new Vector2(-1, 1);
            leftD = new Vector2(1, -1);
            shoots = 0;

            projectileUp = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileUp.GetComponent<Rigidbody2D>().velocity = up * v;

            projectileDown = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileDown.GetComponent<Rigidbody2D>().velocity = down * v;

            projectileRight = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileRight.GetComponent<Rigidbody2D>().velocity = right * v;

            projectileLeft = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileLeft.GetComponent<Rigidbody2D>().velocity = left * v;

            //Diagonal
            projectileUpD = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileUpD.GetComponent<Rigidbody2D>().velocity = upD * v;

            projectileDownD = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileDownD.GetComponent<Rigidbody2D>().velocity = downD * v;

            projectileRightD = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileRightD.GetComponent<Rigidbody2D>().velocity = rightD * v;

            projectileLeftD = Instantiate(prefab, transform.position, Quaternion.identity);
            projectileLeftD.GetComponent<Rigidbody2D>().velocity = leftD * v;


        }
    }
}