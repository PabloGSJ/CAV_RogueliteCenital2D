using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }
    
    public int horizontalMargin;
    public int verticalMargin;

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();


    void Start() // Awake
    {
        //room = GetComponentInParent<Room>();
        //grid.columns = room.Width - horizontalMargin;
        //grid.rows = room.Height - verticalMargin;
        //Debug.Log("Room: " + room.name);
        //GenerateGrid();
    }

    public void GenerateGrid()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - horizontalMargin;
        grid.rows = room.Height - verticalMargin;

        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;
        //List<Vector2> obstacles = room.GetComponentsInChildren<ColisionCoords>()[].availablePoints;

        ColisionCoords colisionCoords = room.GetComponentInChildren<ColisionCoords>();
        //Debug.Log(colisionCoords.availablePoints.Count);
        List<Vector2> obstacles = colisionCoords.availablePoints;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Y: " + y;
                if (!obstacles.Contains(go.transform.position))
                {
                    availablePoints.Add(go.transform.position);
                }
                go.SetActive(false);
            }
        }

        GetComponentInParent<EnemyRoomSpawner>().InitialiseObjectSpawning();
    }
}
