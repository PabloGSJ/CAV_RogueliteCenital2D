using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public int numEnemies = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if((enemies.Count != 0) && (other.CompareTag("Player")))
        {
            RoomController.instance.CurrentRoom.LockDoors();
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
        enemies.Clear();
    }

    void SpawnItems()
    {
        foreach(GameObject item in items)
        {
            item.SetActive(true);
        }
        items.Clear();
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
        numEnemies++;
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    public void enemyDeath()
    {
        numEnemies--;
        if(numEnemies == 0)
        {
            RoomController.instance.CurrentRoom.UnlockDoors();
            SpawnItems();
        }
    }
}
