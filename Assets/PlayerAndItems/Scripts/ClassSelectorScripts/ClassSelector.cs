using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelector : MonoBehaviour
{
    // CONTEXT:

    public GameObject[] Classes;

    public void ClassSelected()
    {
        foreach (GameObject c in Classes)
        {
            c.SetActive(false);
        }
    }
}
