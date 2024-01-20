using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 100f;

    private RoomController rc;
    public Room enemyRoom;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Transform player;


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        enemyRoom = gameObject.transform.parent.gameObject.GetComponent<Room>();
        rc = FindObjectOfType<RoomController>();
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {

        if (rc.CurrentRoom == enemyRoom && player!=null)
        {
            moveDirection = player.position;
            
            Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 finalDirection = (moveDirection - position).normalized;

            rb.velocity = finalDirection * (moveSpeed * Time.fixedDeltaTime); 
        }
        else
        {
           
            rb.velocity = new Vector2(0,0);
        }
    }

}

