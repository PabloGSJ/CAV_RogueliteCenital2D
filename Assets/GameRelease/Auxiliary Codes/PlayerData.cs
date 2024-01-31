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

    public int NGM = 5;
    public GameObject[] weapons;

    // Preservable player context
    private GameObject weapon = null;
    private int health = 1000;
    private int coins = 1000;
    private int bullets = 1000;
    private bool[] activeGM = {false, false, false, false, false};

    // getters-setters
    public int Health { get {return health;} set{health = value;} }
    public int Coins { get {return coins;} set{coins = value;} }
    public int Bullets { get {return bullets;} set{bullets = value;} }
    public bool[] ActiveGM { get {return activeGM;} set{activeGM = value;} }

    public void SetWeapon(GameObject playerWeapon)
    {
        // comparar playerWeapon con weapons[]

        // guardar en weapon el weapons[i] que corresponda
    }

    public GameObject GetWeapon()
    {
        return null;
    }

    public void ResetPlayerData()
    {
        weapon = null;
        health = 10;
        coins = 0;
        bullets = 10;
        
        for (int i = 0; i < activeGM.Length; i++)
        {
            activeGM[i] = false;
        }
    }
}
