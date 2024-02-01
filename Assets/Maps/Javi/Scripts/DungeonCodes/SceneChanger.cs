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
            StartSceneChange();          
        }
    }

    public void StartSceneChange()
    {
        GetComponentInParent<RoomController>().StopLevelMusic();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneNames[(int)sceneName]));

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNames[(int)sceneName + 1], LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames[(int)sceneName + 1]));
    }
}
