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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<EnemyStateMachine>();
        ChangeTargetInt();
    }

    private void Start()
    {
        targetPoint = 0;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * waypointRadius;
            randomOffset.z = 0f; 

            Vector3 randomWaypointPosition = enemySpawnPoint.position + randomOffset;

            patrolPoints[i].position = randomWaypointPosition;
        }
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
                    collisionToWall(false);
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

    public void collisionToWall(bool collision)
    {
        isWall = collision;
    }
}
