using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public SceneType sceneName;
    bool loading = false;
    GameObject player;
    GameObject playerData;
    PlayerData data;

    string[] SceneNames = new string[]
    {
        "HomeBase",
        "Forest",
        "City",
        "Building",
        "Sewers",
        "BossStage"
    };

    public enum SceneType
    {
        HomeBase,
        Forest,
        City,
        Building,
        Sewers,
        BossStage
    }    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.tag == "Player" && !loading)
        {
            temp.GetComponent<PlayerStateMachine>().SaveState();
            loading = true;
            //StartSceneChange();
        }
    }

    public void StartSceneChange()
    {
        GetComponentInParent<RoomController>().StopLevelMusic();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        AsyncOperation asyncLoadingScene = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        while (!asyncLoadingScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneNames[(int)sceneName]));

        AsyncOperation asyncNextScene = SceneManager.LoadSceneAsync(SceneNames[(int)sceneName + 1], LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
        while (!asyncNextScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames[(int)sceneName + 1]));
    }

    public void RestartGame()
    {
        SceneManager.GetSceneByName("MainScene").GetRootGameObjects()[0].GetComponent<PlayerData>().ResetPlayerData();
        StartCoroutine(LoadBeginning());
    }

    IEnumerator LoadBeginning()
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneNames[(int)sceneName]));

        AsyncOperation asyncStartScene = SceneManager.LoadSceneAsync(SceneNames[0], LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
        while (!asyncStartScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames[0]));
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MainScene"));
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneNames[(int)sceneName]));

        AsyncOperation asyncLoadMenu = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        while (!asyncLoadMenu.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
    }
}
