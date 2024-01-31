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
    private int health = 10;
    private int coins = 0;
    private int bullets = 10;
    private bool[] activeGM = {false, false, false, false, false};

    // getters-setters
    public int Health { get {return health;} set{health = value;} }
    public int Coins { get {return coins;} set{coins = value;} }
    public int Bullets { get {return bullets;} set{bullets = value;} }
    public bool[] ActiveGM { get {return activeGM;} set{activeGM = value;} }

    public void SetWeapon(BaseWeapon playerWeapon)
    {
        // comparar playerWeapon con weapons[]
        // foreach (GameObject weapon in weapons)
        // {
        //     if (playerWeapon.name == weapon.name)
        //     {
        //         this.weapon = weapon;
        //         Debug.Log("Weapon set");
        //         break;
        //     }
        //     else
        //     {
        //         this.weapon = null;
        //     }
        // }

        // BaseWeapon bw =  weapons[0].GetComponent<BaseWeapon>();

        // if (bw is WeaponCrowbar)
        // {
        //     Debug.Log("Si lo es");
        // }

        // if (bw is WeaponGun)
        // {
        //     Debug.Log("Si lo es");
        // }
        // else
        // {
        //     Debug.Log("No lo es");
        // }

        // Debug.Log("Writing weapon");

        // if (playerWeapon == null)
        // {
        //     weapon = null; // KnuckleBuster
        //     Debug.Log("KnuckleBuster set");
        // }
        // else if (playerWeapon.GetComponent<WeaponCrowbar>() != null)
        // {
        //     weapon = weapons[0];
        // }
        // else if (playerWeapon.GetComponent<WeaponGun>() != null)
        // {
        //     weapon = weapons[1];
        //     Debug.Log("Weapon set");
        // }
        // else if (playerWeapon.GetComponent<WeaponShotgun>() != null)
        // {
        //     weapon = weapons[2];
        // }
        // else
        // {
        //     weapon = weapons[3];
        //     Debug.Log("Default weapon set");
        // }

        if (playerWeapon is WeaponCrowbar)
        {
            weapon = weapons[0];
            // Debug.Log("Crowbar set");
        }
        else if (playerWeapon is WeaponGun)
        {
            weapon = weapons[1];
            // Debug.Log("Gun set");
        }
        else if (playerWeapon is WeaponShotgun)
        {
            weapon = weapons[2];
            // Debug.Log("Shotgun set");
        }
        else if (playerWeapon is WeaponSniper)
        {
            weapon = weapons[3];
            // Debug.Log("Sniper set");
        }
        else
        {
            weapon = null;
            // Debug.Log("KnuckleBuster set");
        }


        // for (int i = 0; i < weapons.Length; i++)
        // {
        //     if (playerWeapon.name == weapons[i].name)
        //     {
        //         weapon = weapons[i];
        //         Debug.Log("Weapon set");
        //         break;
        //     }
        //     else
        //     {
        //         weapon = null;
        //     }
        // }

        // guardar en weapon el weapons[i] que corresponda
    }

    public GameObject GetWeapon()
    {
        return weapon;
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
