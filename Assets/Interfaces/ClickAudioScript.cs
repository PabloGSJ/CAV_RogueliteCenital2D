using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAudioScript : MonoBehaviour
{
    public AudioSource click;

    public void playClickSoundEffect()
    {
        click.Play();
    }
}
