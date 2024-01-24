using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Collider2D mycol;
    public SpriteRenderer sr;
    private SoundControllerScript sc;

    public Sprite[] spriteList;
    private const int Open = 1;
    private const int Closed = 0;

    public GameObject[] possibleWeapons;
    public GameObject[] possibleGameModifiers;


    private void Awake()
    {
        sr.sprite = spriteList[Closed];

        sc = GameObject.Find("SoundControl").GetComponent<SoundControllerScript>();
    }

    // called when opening the chest
    public BaseGM OpenChest(PlayerStateMachine player)
    {
        sc.playChestOpenSoundEffect();

        mycol.enabled = false;
        sr.sprite = spriteList[Open];

        // Drop weapon
        Instantiate(possibleWeapons[Random.Range(0, possibleWeapons.Length)],
                    new Vector3(this.transform.position.x,
                                this.transform.position.y - 1.5f,
                                this.transform.position.z),
                    this.transform.rotation);

        BaseGM gm = null;
        if (Random.Range(0, 100) < 50)
        {
            // Give game modifier
            
        }

        return gm;
    }
}
