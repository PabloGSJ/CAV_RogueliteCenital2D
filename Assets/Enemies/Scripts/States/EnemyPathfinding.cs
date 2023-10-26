using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private EnemyStateMachine stateMachine;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void FixedUpdate()
    {
       
        if (stateMachine.IsRoaming) 
        {
            Debug.Log("MOVIMIENTO ROAMING");
            rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        if (stateMachine.IsChasing)
        {
            Debug.Log("MOVIMIENTO CHASING");
            //Arreglar esta parte (se mueve mal)
            rb.MovePosition(moveDirection * (moveSpeed * Time.fixedDeltaTime));
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
}
