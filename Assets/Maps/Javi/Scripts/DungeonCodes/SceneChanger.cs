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

    // void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }
    void Start()
    {
        // playerData = SceneManager.GetSceneByName("MainScene").GetRootGameObjects()[0];
        // data = playerData.GetComponent<PlayerData>();
        // Debug.Log(playerData.GetComponent<PlayerData>().Bullets);
    }
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.tag == "Player" && !loading)
        {
            // player = temp;
            // if (player != null)
            // {
            //     Debug.Log("Player gotten");
            // player = temp;
            // }
            temp.GetComponent<PlayerStateMachine>().SaveState();
            loading = true;

            // data.Weapon = temp.GetComponent<PlayerStateMachine>().Weapon.GetComponent<BaseWeapon>(); //TODO: No la pilla bien
            // data.Health = temp.GetComponent<PlayerStateMachine>().Health;
            // data.Coins = temp.GetComponent<PlayerStateMachine>().Coins;
            // data.Bullets = temp.GetComponent<PlayerStateMachine>().NumBullets;
            // Debug.Log("Player entered");  
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
        // yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneNames[(int)sceneName]));

        // Debug.Log("Reached here");
        // Debug.Log("Reached here");
        // Debug.Log("Reached here");
        // Debug.Log("Reached here");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNames[(int)sceneName + 1], LoadSceneMode.Additive);
        // Debug.Log("Reached here");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // Debug.Log("Reached here");
        // Scene otherScene = SceneManager.GetSceneByName(SceneNames[(int)sceneName]);
        // GameObject[] newSceneObjects = otherScene.GetRootGameObjects();
        // GameObject otherPlayer = null;
        // foreach (GameObject obj in newSceneObjects)
        // {
        //     if (obj.tag == "Player")
        //     {
        //         Debug.Log("Other Player found");
        //         otherPlayer = obj;
        //         break;
        //     }
        // }

        // otherPlayer.GetComponent<PlayerStateMachine>().Health = player.GetComponent<PlayerStateMachine>().Health;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames[(int)sceneName + 1]));
    }
}
