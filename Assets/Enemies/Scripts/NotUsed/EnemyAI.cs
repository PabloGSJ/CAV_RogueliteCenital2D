using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    private RoomController rc;
    public Room enemyRoom;

    private EnemyPathfinding enemyPathfinding;
    private EnemyStateMachine stateMachine;

    private void Awake()
    {

        enemyRoom = gameObject.transform.parent.gameObject.GetComponent<Room>();
        rc = FindObjectOfType<RoomController>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    public IEnumerator RoamingRoutine()
    {
        Debug.Log("CURRENT ROOM: "+ rc.CurrentRoom);
        while (true)
        {
            if (rc.CurrentRoom == enemyRoom) { 
                Debug.Log("Roaming");
                Vector2 roamPosition = GetRoamingPosition();
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public IEnumerator ChasingRoutine()
    {
        while (true)
        {
            Debug.Log("Chasing");
            Vector2 roamPosition = GetChasingPosition();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector2 GetRoamingPosition() 
    {
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f)).normalized; 
    }

    private Vector2 GetChasingPosition()
    {
        return stateMachine.Player.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && stateMachine.IsRoaming) 
        {
            StopAllCoroutines();
            StartCoroutine(RoamingRoutine());
        }
    }

}
