using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlash : MonoBehaviour
{
    public float TTL;

    private void Update()
    {
        // die instantly
        if (TTL <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            TTL -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
