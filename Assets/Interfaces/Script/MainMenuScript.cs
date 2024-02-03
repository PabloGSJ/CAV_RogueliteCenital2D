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
        sc.StopMainMenuMusic();
        StartCoroutine(LoadGame());
        
    }

    IEnumerator LoadGame()
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MainMenu"));

        AsyncOperation asyncStartScene = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
        while (!asyncStartScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
    }

    public void Quit()
    {
        Application.Quit();
    }

}
