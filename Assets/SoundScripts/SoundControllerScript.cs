using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    public AudioSource coinSound;

    public void playCoinSoundEffect()
    {
        Debug.Log("SUENA MONEDA");
        coinSound.Play();
    }
}
