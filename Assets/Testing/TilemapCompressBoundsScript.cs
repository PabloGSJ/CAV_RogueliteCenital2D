using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TilemapCompressBoundsScript : MonoBehaviour
{
    //private Tilemap _t;
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Tilemap>().CompressBounds();
    }
}