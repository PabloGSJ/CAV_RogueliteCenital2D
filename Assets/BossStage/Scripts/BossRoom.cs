using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossRoom : MonoBehaviour
{
    public CinemachineVirtualCamera cmvcam;
    public BossStateMachine boss;
    public GameObject cat;
    public GameObject wincanvas;
    public Animator transition;
    private SoundControllerScript sc;

    private const int PlayerLayer = 6;
    private const int PlayerDashingLayer = 14;

    // Door
    private bool _doorClosed;
    public GameObject Door;

    private void Awake()
    {
        cmvcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        sc = GameObject.Find("SoundControl").GetComponent<SoundControllerScript>();
        Door.SetActive(false);
        _doorClosed = false;
        cat.SetActive(false);
        wincanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.CompareTo("Player") == 0) // PlayerLayer
        {
            sc.playEvilLaughSoundEffect();
            sc.playBossMusic();

            cmvcam.Follow = this.transform;
            Door.SetActive(true);
            _doorClosed = true;
            boss.IsSleeping = false;
        }
    }

    public void BossDead()
    {
        sc.stopBossMusic();
        cat.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        wincanvas.SetActive(true);
        transition.SetTrigger("Fade");
    }
}
