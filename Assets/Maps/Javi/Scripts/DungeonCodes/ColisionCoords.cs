using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColisionCoords : MonoBehaviour
{

    public Room room;

    [System.Serializable]
    public struct ColGrid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    public GridController gridController;

    public int horizontalMargin;
    public int verticalMargin;

    public ColGrid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();

    void Awake() 
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - horizontalMargin;
        grid.rows = room.Height - verticalMargin;
        GetCoords();
    }

    public void GetCoords()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                

                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) 
                {
                    GameObject go = Instantiate(gridTile, transform);
                    go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                    go.name = "X: " + x + ", Y: " + y;
                    availablePoints.Add(go.transform.position);
                    go.SetActive(false);
                }
            }
        }  

        gridController.GenerateGrid();   
    }
}
