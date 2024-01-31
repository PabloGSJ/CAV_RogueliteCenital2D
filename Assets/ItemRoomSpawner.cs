using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoomSpawner : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] SpawnerData;
    public FightController fightController;

    public void InitialiseObjectSpawning()
    {
        foreach(RandomSpawner rs in SpawnerData)
        {
            SpawnObject(rs);
        }
    }

    void SpawnObject(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
            fightController.AddItem(go);
            go.SetActive(false);
        }
    }
}
