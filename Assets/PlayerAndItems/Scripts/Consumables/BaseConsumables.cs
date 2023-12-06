using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConsumables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Picked me up");
        Destroy(gameObject);
    }
}
