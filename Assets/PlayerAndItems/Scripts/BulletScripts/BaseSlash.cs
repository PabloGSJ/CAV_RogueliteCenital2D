using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlash : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Live());
    }

    private IEnumerator Live()
    {
        yield return new WaitForSeconds(1f);    // Stay on screen 
        Destroy(gameObject);
    }
}
