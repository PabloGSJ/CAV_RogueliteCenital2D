using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    private AudioSource coinSound;
    private AudioSource heartSound;
    private AudioSource healthPotionSound;

    public void Start()
    {
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        heartSound = GameObject.Find("HeartSound").GetComponent<AudioSource>();
        heartSound = GameObject.Find("HealthPotionSound").GetComponent<AudioSource>();
    }


    public void playCoinSoundEffect()
    {
        coinSound.Play();
    }

    public void playHeartSoundEffect()
    {
        heartSound.Play();
    }

    public void playHealthPotionSoundEffect()
    {
        healthPotionSound.Play();
    }
}
