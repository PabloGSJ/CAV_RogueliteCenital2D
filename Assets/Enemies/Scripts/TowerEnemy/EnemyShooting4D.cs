using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting4D : MonoBehaviour
{
    public GameObject prefab;

    public Room enemyRoom; 
    private RoomController rc;

    public float cd;
    public float v;
    private float timer;
    private int shoots;

    public void Start()
    {
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

            if (timer <= 0f)
            {
                shoots += 1;
                Shoot4D();
                timer = cd;
            }
        }
        
    }

    private void Shoot4D()
    {
        Vector2 up;
        Vector2 down;
        Vector2 left;
        Vector2 right;

        if (shoots <= 1)
        {
            up = Vector2.up.normalized;
            down = Vector2.down.normalized;
            right = Vector2.right.normalized;
            left = Vector2.left.normalized;
        } else
        {
            up = new Vector2(1,1);
            down = new Vector2(-1, -1);
            right = new Vector2(-1, 1);
            left = new Vector2(1, -1);
            shoots = 0;
        }

        GameObject projectileUp = Instantiate(prefab,transform.position,Quaternion.identity);
        projectileUp.GetComponent<Rigidbody2D>().velocity = up * v;

        GameObject projectileDown = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileDown.GetComponent<Rigidbody2D>().velocity = down * v;

        GameObject projectileRight = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileRight.GetComponent<Rigidbody2D>().velocity = right * v;

        GameObject projectileLeft = Instantiate(prefab, transform.position, Quaternion.identity);
        projectileLeft.GetComponent<Rigidbody2D>().velocity = left * v;
    }
}
