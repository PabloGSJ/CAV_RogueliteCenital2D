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
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom("1", roomLocation.x, roomLocation.y);
        }
    }
}
