using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
        // Debug.Log("Loading HomeBase");
        // StartGame();
        StartCoroutine(StartGame());
        // AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync("HomeBase", LoadSceneMode.Additive);

    }

    IEnumerator StartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HomeBase", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // SceneManager.UnloadSceneAsync("Loading");
        // SceneManager.LoadScene("HomeBase");
        // Debug.Log("Loading HomeBase");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("HomeBase"));
        // Debug.Log("HomeBase Loaded");
    }

    public BaseWeapon weapon = null;
    public int health = 10;
    public int coins = 0;
    public int bullets = 99;
    public BaseWeapon Weapon { get {return weapon;} set{weapon = value;} }
    public int Health { get {return health;} set{health = value;} }
    public int Coins { get {return coins;} set{coins = value;} }
    public int Bullets { get {return bullets;} set{bullets = value;} }
}
