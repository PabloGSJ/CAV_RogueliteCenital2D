using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public Text PlayerHealthDisplay;
    public Text PlayerNumBulletsDisplay;
    public Text WeaponNumBulletsDisplay;
    public Text PlayerCoinsDisplay;

    public void DisplayNewHealth(int newHealth)
    {
        PlayerHealthDisplay.text = newHealth.ToString();
    }

    public void DisplayNewPNBullets(int newNumBullets)
    {
        PlayerNumBulletsDisplay.text = newNumBullets.ToString();
    }

    public void DisplayNewWNBullets(int newNumBullets)
    {
        WeaponNumBulletsDisplay.text = newNumBullets.ToString();
    }

    public void EnableWeaponNBullets(bool enable)
    {
        WeaponNumBulletsDisplay.gameObject.SetActive(enable);
    }

    public void DisplayNewPCoins(int newNumCoins)
    {
        PlayerCoinsDisplay.text = newNumCoins.ToString();
    }
}
