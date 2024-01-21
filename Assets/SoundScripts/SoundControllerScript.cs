using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    private AudioSource coinSound;
    private AudioSource heartSound;
    private AudioSource healthPotionSound;

    public void Start()
    {
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        heartSound = GameObject.Find("HeartSound").GetComponent<AudioSource>();
        healthPotionSound = GameObject.Find("HealthPotionSound").GetComponent<AudioSource>();

        if (coinSound != null && heartSound != null&& heartSound != null)
        {
            Debug.Log("No problem here");
        }
    }


    public void playCoinSoundEffect()
    {
        coinSound.Play();
    }

    public void playHeartSoundEffect()
    {
        heartSound.Play();
    }

    public void playHealthPotionSoundEffect()
    {
        healthPotionSound.Play();
    }

    // TODO:
    // PLAYER
    public void playPlayerDamagedSoundEffect() { }
    public void playGameOverTuneSoundEffect() { }
    public void playRunSoundEffect() { }
    public void playDashSoundEffect() { }           // when player presses dash key
    public void playHitAWallSoundEffect() { }

    // WEAPONS
    public void playGunshotSoundEffect() { }
    public void playSwordSwingSoundEffect() { }
    public void playPunchSoundEffect() { }
    public void playPickupWeaponSoundEffect() { }
    public void playBulletImpactSoundEffect() { }   // when bullets hit a wall
    
    // CONSUMABLES
    public void playBulletsGroundSoundEffect() { }  // pickup GroundBullets
    public void playBulletRoundSoundEffect() { }    // pickup BulletRound

    // MAP
    // Starting house
    public void playExitHouseSoundEffect() { }
    public void playJoyfulSoundEffect() { }
    // Zone travelling
    public void playDoorOpeningSoundEffect() { }
    public void playCarKickstartSoundEffect() { }
    public void playStairsDownSoundEffect() { }
    // Entities
    public void playChestOpenSoundEffect() { }
    public void playBuyShopSoundEffect() { }

    // ENEMIES

    // BOSS
    public void playEvilLaughSoundEffect() { }
    public void playEnergyProjectileShotSoundEffect() { }
    public void playFireBulletSoundEffect() { }
    // more may come in the future, when the boss is finished
}
