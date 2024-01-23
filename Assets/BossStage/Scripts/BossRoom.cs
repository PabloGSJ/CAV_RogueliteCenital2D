using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossRoom : MonoBehaviour
{
    public CinemachineVirtualCamera cmvcam;
    public BossStateMachine boss;

    private const int PlayerLayer = 6;
    private const int PlayerDashingLayer = 14;

    // Door
    private bool _doorClosed;
    public GameObject Door;

    private void Awake()
    {
        cmvcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        Door.SetActive(false);
        _doorClosed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.CompareTo("Player") == 0) // PlayerLayer
        {
            cmvcam.Follow = this.transform;
            Door.SetActive(true);
            _doorClosed = true;
            boss.IsSleeping = false;
        }
    }
}
