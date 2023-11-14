using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    // State variables
    private EnemyBaseState currentState;
    private EnemyStateFactory states;

    public float chaseDistance = 10;
    private Transform player;
    private float distanceToPlayer;
    private bool closeToPlayer;
    private bool isRoaming;
    private bool isChasing;

    //getters and setters
    public EnemyBaseState CurrentState { get { return currentState; } set { currentState = value; }}
    public bool CloseToPlayer { get { return closeToPlayer; }}
    public bool IsRoaming { get { return isRoaming; } set { isRoaming = value; } }
    public bool IsChasing { get { return isChasing; } set { isChasing = value; } }
    public Transform Player { get { return player; } }

    private void Awake()
    {
        EnemyAI enemyAI = GetComponent<EnemyAI>();

        states = new EnemyStateFactory(this, enemyAI);
        currentState = states.Roaming();
        currentState.EnterState();

        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        PlayerClose();
        currentState.UpdateState();
    }

    private void PlayerClose()
    {
        if (distanceToPlayer <= chaseDistance)
        {
            closeToPlayer = true;
        } else
        {
            closeToPlayer = false;
        }
    }

    //Only for debug, delete before building the game
    private void OnDrawGizmos()
    {
        // draw the chase distance 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,chaseDistance); 
    }
}
