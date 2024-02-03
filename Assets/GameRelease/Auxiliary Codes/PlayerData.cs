using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartGame());
        if (SceneManager.GetSceneByName("Loading").isLoaded)
        {
            Debug.Log("Despawning Loading");
            StartCoroutine(DespawnLoading());
        }
    }

    IEnumerator DespawnLoading()
    {
        AsyncOperation unloadLoading = SceneManager.UnloadSceneAsync("Loading");
        while (!unloadLoading.isDone)
        {
            yield return null;
        }
    }

    IEnumerator StartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HomeBase", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("HomeBase"));
    }

    public int NGM = 5;
    public GameObject[] weapons;

    // Constants
    public const int START_HEALTH = 10;
    public const int START_COINS = 0;
    public const int START_BULLETS = 10;

    // Preservable player context
    private GameObject weapon = null;
    private int health = START_HEALTH;
    private int coins = START_COINS;
    private int bullets = START_BULLETS;
    private bool[] activeGM = {false, false, false, false, false};

    // getters-setters
    public int Health { get {return health;} set{health = value;} }
    public int Coins { get {return coins;} set{coins = value;} }
    public int Bullets { get {return bullets;} set{bullets = value;} }
    public bool[] ActiveGM { get {return activeGM;} set{activeGM = value;} }

    public void SetWeapon(BaseWeapon playerWeapon)
    {
        if (playerWeapon is WeaponCrowbar)
        {
            weapon = weapons[0]; // Crowbar
        }
        else if (playerWeapon is WeaponGun)
        {
            weapon = weapons[1]; // Gun
        }
        else if (playerWeapon is WeaponShotgun)
        {
            weapon = weapons[2]; // Shotgun
        }
        else if (playerWeapon is WeaponSniper)
        {
            weapon = weapons[3]; // Sniper
        }
        else
        {
            weapon = null; // KnuckleBuster
        }
    }

    public GameObject GetWeapon()
    {
        return weapon;
    }

    public void ResetPlayerData()
    {
        weapon = null;
        health = START_HEALTH;
        coins = START_COINS;
        bullets = START_BULLETS;
        
        for (int i = 0; i < activeGM.Length; i++)
        {
            activeGM[i] = false;
        }
    }
}
