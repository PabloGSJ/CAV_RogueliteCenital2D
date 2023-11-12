using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentScript : MonoBehaviour
{

    private void Awake()
    {
        gameObject.transform.SetParent(p: null);
    }
}
