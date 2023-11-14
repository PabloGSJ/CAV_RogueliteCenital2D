using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyInstances : MonoBehaviour
{
    
    public GameObject myPrefab;
    public int numberOfEnemies;
    public Transform[] patrolPoints;
    public int numberOfPatrolPoints;

    void Awake()
    {
        Transform[] patrolPoints = new Transform[numberOfPatrolPoints];

        for (int i = 0; i < numberOfPatrolPoints; i++)
        {
            
            patrolPoints[i] = new GameObject("wayPoint" + i).transform;
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemyInstance = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            EnemyPathfinding enemyScript = enemyInstance.GetComponent<EnemyPathfinding>();

            if (enemyScript != null)
            {
                enemyScript.SetPatrolPoints(patrolPoints);
            }
        }
    }
}

