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
        RoomController.instance.LoadRoom("CopiaIni", 0, 0); // TODO: se debe llamar Start
        var random = new System.Random();
        foreach (Vector2Int roomLocation in rooms)
        {
            int numRoom = random.Next(1, 5);
            RoomController.instance.LoadRoom(numRoom.ToString(), roomLocation.x, roomLocation.y);
        }
    }
}
