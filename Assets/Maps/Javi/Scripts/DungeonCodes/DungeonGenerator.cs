using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        var random = new System.Random();
        foreach (Vector2Int roomLocation in rooms)
        {
            int numRoom = random.Next(1, 8);
            RoomController.instance.LoadRoom(numRoom.ToString(), roomLocation.x, roomLocation.y);
        }
    }
}
