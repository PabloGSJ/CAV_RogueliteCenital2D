using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSorting : MonoBehaviour
{
    public SpriteRenderer characterRenderer;
    public SpriteRenderer treeRenderer;
    public SpriteRenderer characterRenderer1;

    void Update()
    {
        if (characterRenderer.transform.position.y < treeRenderer.transform.position.y)
        {
            characterRenderer.sortingOrder = treeRenderer.sortingOrder + 1;
            characterRenderer1.sortingOrder = treeRenderer.sortingOrder + 1;
        }
        else
        {
            characterRenderer.sortingOrder = treeRenderer.sortingOrder - 1;
            characterRenderer1.sortingOrder = treeRenderer.sortingOrder - 1;
        }
    }
}
