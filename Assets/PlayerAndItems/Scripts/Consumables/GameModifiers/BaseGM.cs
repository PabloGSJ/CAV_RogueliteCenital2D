using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGM : MonoBehaviour
{
    protected GMManager gmm;

    private void Awake()
    {
        gmm = GameObject.FindGameObjectWithTag("GameModifiersManager").GetComponent<GMManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Picked me up");
        ApplyEffect();
        Destroy(gameObject);
    }

    protected abstract void ApplyEffect();
}
