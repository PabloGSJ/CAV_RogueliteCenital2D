using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private EnemyStateMachine stateMachine;
    private Vector2 moveDirection;

    public Transform[] patrolPoints;
    public int targetPoint;
    private bool isWall;

    public Transform enemySpawnPoint;
    public float waypointRadius;

    public bool IsWall { get { return isWall; } set { isWall = value; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<EnemyStateMachine>();
        ChangeTargetInt();
        targetPoint = 0;
    }

    private void FixedUpdate()
    {

        if (stateMachine.IsRoaming)
        {
            
            if (!isWall)
            { 
                rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
            } else
            {
                if (transform.position == patrolPoints[targetPoint].position)
                {
                    isWall = false;
                    ChangeTargetInt();
                }
                Debug.Log("Target: " + targetPoint);

                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, moveSpeed * Time.deltaTime);
                
            }
            
        }

        if (stateMachine.IsChasing)
        {
            Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);
            rb.MovePosition(position + ((moveDirection - position) * (moveSpeed * Time.deltaTime)));
        }
    }

    void ChangeTargetInt()
    {
        targetPoint = Random.Range(0, patrolPoints.Length);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
}
