using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Give this script to any grid with tilemap children

[ExecuteInEditMode]
public class TilemapCompressBoundsScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        foreach (Tilemap t in this.GetComponentsInChildren<Tilemap>())
        {
            t.CompressBounds();
        }
    }
}