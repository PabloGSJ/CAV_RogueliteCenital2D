using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    // Displays
    public Text PlayerNumBulletsDisplay;
    public Text WeaponNumBulletsDisplay;
    public Text PlayerCoinsDisplay;
    public Text DashCooldownDisplay;
    public Text IsGodDisplay;

    // Health display
    public Image[] healthImages;            // Images in the canvas to allocate the 
    public Sprite[] heartSprites;           // Posible heart sprites (Empty, Half and Full)
    private const int Empty = 2;
    private const int Half = 1;
    private const int Full = 0;
    public int MaxHealth;
    private int _activeHearts;
    public int HealthPerHeart = 2;

    public int ActiveHearts { get { return _activeHearts; } set { _activeHearts = value; } }

    public void DisplayNewHealth(int newHealth)
    {
        int i = 0;
        foreach (Image image in healthImages)
        {
            if (i > _activeHearts)
            {
                // Heart is not active
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
                // check which heart it has to draw
                if (newHealth - i * HealthPerHeart > 1)
                {
                    image.sprite = heartSprites[Full];
                }
                else if (newHealth - i * HealthPerHeart == 1)
                {
                    image.sprite = heartSprites[Half];
                }
                else
                {
                    image.sprite = heartSprites[Empty];
                }
            }
            i++;
        }
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

    public void DisplayNewDashCooldown(float newDashCooldown)
    {
        DashCooldownDisplay.text = newDashCooldown.ToString();
    }

    public void EnableDashCooldown(bool enable)
    {
        DashCooldownDisplay.gameObject.SetActive(enable);
    }

    public void EnableIsGodDisplay(bool enable)
    {
        IsGodDisplay.gameObject.SetActive(enable);
    }
}
