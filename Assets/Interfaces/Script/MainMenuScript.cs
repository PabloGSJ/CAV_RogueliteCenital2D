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
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != "MainMenu")
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
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
