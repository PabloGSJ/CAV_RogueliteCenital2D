using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectileTower : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(waitAndDestroy());
    }

    IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 11)
        {
            Destroy(gameObject);
        }
    }
}

