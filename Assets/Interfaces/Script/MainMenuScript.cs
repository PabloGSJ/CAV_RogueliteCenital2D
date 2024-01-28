using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private SoundControllerScript sc;

    public void Awake()
    {
        sc = GameObject.Find("SoundControl").GetComponent<SoundControllerScript>();
    }

    public void Start()
    {
        sc.playMainMenuMusic();
    }
    public void Play()
    {
        //SceneManager.LoadScene()
        sc.StopMainMenuMusic();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
