using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConsumables : MonoBehaviour
{
    protected SoundControllerScript sc;

    public abstract void UseConsumable(PlayerStateMachine player);

    // public void Start()
    // {
    //     sc = GameObject.Find("SoundControl").GetComponent<SoundControllerScript>();
    // }

    private void Awake()
    {
        sc = GameObject.Find("SoundControl").GetComponent<SoundControllerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateMachine player = collision.gameObject.GetComponent<PlayerStateMachine>();
        if (player != null)
        {
            UseConsumable(player);
            Debug.Log("Picked me up");
            Destroy(gameObject);
        }
    }
}
