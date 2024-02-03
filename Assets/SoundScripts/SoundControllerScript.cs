using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    //SoundEffects
    private AudioSource coinSound;
    private AudioSource heartSound;
    private AudioSource healthPotionSound;
    private AudioSource dashSound;
    private AudioSource auchPlayerSound;
    private AudioSource buySound;
    private AudioSource chestOpenSound;
    private AudioSource noWeaponHitSound;
    private AudioSource swordSound;
    private AudioSource gunSound;
    private AudioSource pickupWeaponSound;
    private AudioSource walkSound;
    private AudioSource joyFulSound;
    private AudioSource energySound;
    private AudioSource gameOverSound;
    private AudioSource doorOpenSound;
    private AudioSource doorCloseSound;
    private AudioSource stairsSound;
    private AudioSource hitWallSound;
    private AudioSource carStartingSound;
    private AudioSource evilLaughSound;
    private AudioSource pickupBulletsSound;
    private AudioSource enemyDeathSound;
    private AudioSource enemyAttackSound;

    //Music
    private AudioSource startMusic;
    private AudioSource forestMusic;
    private AudioSource cityMusic;
    private AudioSource houseMusic;
    private AudioSource sewersMusic;
    private AudioSource airMusic;
    private AudioSource bossMusic;
    private AudioSource mainMenuMusic;


    public void Awake()
    {
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        heartSound = GameObject.Find("HeartSound").GetComponent<AudioSource>();
        healthPotionSound = GameObject.Find("HealthPotionSound").GetComponent<AudioSource>();
        dashSound = GameObject.Find("DashSound").GetComponent<AudioSource>();
        auchPlayerSound =  GameObject.Find("AuchPlayerSound").GetComponent<AudioSource>();
        buySound = GameObject.Find("BuySound").GetComponent<AudioSource>();
        chestOpenSound = GameObject.Find("ChestOpenSound").GetComponent<AudioSource>();
        noWeaponHitSound = GameObject.Find("NoWeaponHitSound").GetComponent<AudioSource>();
        swordSound = GameObject.Find("SwordSound").GetComponent<AudioSource>();
        gunSound = GameObject.Find("GunSound").GetComponent<AudioSource>();
        pickupWeaponSound = GameObject.Find("PickupWeaponSound").GetComponent<AudioSource>();
        walkSound = GameObject.Find("WalkSound").GetComponent<AudioSource>();
        joyFulSound = GameObject.Find("JoyFulSound").GetComponent<AudioSource>();
        energySound = GameObject.Find("EnergySound").GetComponent<AudioSource>();
        gameOverSound = GameObject.Find("GameOverSound").GetComponent<AudioSource>();
        doorOpenSound = GameObject.Find("DoorOpenSound").GetComponent<AudioSource>();
        doorCloseSound = GameObject.Find("DoorCloseSound").GetComponent<AudioSource>();
        stairsSound = GameObject.Find("StairsSound").GetComponent<AudioSource>();
        hitWallSound = GameObject.Find("HitWallSound").GetComponent<AudioSource>();
        carStartingSound = GameObject.Find("CarStartingSound").GetComponent<AudioSource>();
        evilLaughSound = GameObject.Find("EvilLaughSound").GetComponent<AudioSource>();
        pickupBulletsSound = GameObject.Find("PickupBulletsSound").GetComponent<AudioSource>();
        enemyDeathSound = GameObject.Find("EnemyDeathSound").GetComponent<AudioSource>();
        enemyAttackSound = GameObject.Find("EnemyAttackSound").GetComponent<AudioSource>();


        //Music
        forestMusic = GameObject.Find("ForestMusic").GetComponent<AudioSource>();
        cityMusic = GameObject.Find("CityMusic").GetComponent<AudioSource>();
        houseMusic = GameObject.Find("HouseMusic").GetComponent<AudioSource>();
        sewersMusic = GameObject.Find("SewersMusic").GetComponent<AudioSource>();
        airMusic = GameObject.Find("AirMusic").GetComponent<AudioSource>();
        bossMusic = GameObject.Find("BossMusic").GetComponent<AudioSource>();
        mainMenuMusic = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        startMusic = GameObject.Find("StartMusic").GetComponent<AudioSource>();
    }

    // PLAYER
    public void playPlayerDamagedSoundEffect() 
    {
        auchPlayerSound.Play();
    }
    public void playGameOverTuneSoundEffect() 
    { 
        gameOverSound.Play();   
    }
    public void playRunSoundEffect() 
    { 
        walkSound.Play();
    }

    public void stopRunSoundEffect()
    {
        walkSound.Stop();
    }

    public void playDashSoundEffect() 
    {
        // when player presses dash key
        dashSound.Play();
    }           
    public void playHitAWallSoundEffect() { }

    // WEAPONS
    public void playGunshotSoundEffect() 
    {
        gunSound.Play();
    }
    public void playSwordSwingSoundEffect() 
    { 
        swordSound.Play();
    }
    public void playPunchSoundEffect() 
    { 
        noWeaponHitSound.Play();    
    }
    public void playPickupWeaponSoundEffect() 
    {
        pickupWeaponSound.Play();   
    }
    public void playBulletImpactSoundEffect() 
    { 
        hitWallSound.Play();
    }   // when bullets hit a wall
    
    // CONSUMABLES
    public void playBulletsGroundSoundEffect() 
    {
        pickupBulletsSound.Play();
    }  // pickup GroundBullets
    public void playBulletRoundSoundEffect() 
    {
        pickupBulletsSound.Play();  
    }    // pickup BulletRound

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

    // MAP
    // Starting house
    public void playExitHouseSoundEffect() 
    { 
        doorCloseSound.Play();
    }
    public void playJoyfulSoundEffect() 
    { 
        joyFulSound.Play(); 
    }
    // Zone travelling
    public void playDoorOpeningSoundEffect() 
    { 
        doorOpenSound.Play();
    }
    public void playCarKickstartSoundEffect() 
    { 
        carStartingSound.Play();
    }
    public void playStairsDownSoundEffect() 
    { 
        stairsSound.Play();
    }

    // Entities
    public void playChestOpenSoundEffect() 
    { 
        chestOpenSound.Play();  
    }
    public void playBuyShopSoundEffect() 
    { 
        buySound.Play();
    }

    // ENEMIES

    // BOSS
    public void playEvilLaughSoundEffect() 
    { 
       evilLaughSound.Play();
    }
    public void playEnergyProjectileShotSoundEffect() 
    {
        energySound.Play();
    }

    // more may come in the future, when the boss is finished

    //Common Enmies
    public void playEnemyDeathSound()
    {
        enemyDeathSound.Play();
    }

    public void playEnemyAttackSound()
    {
        enemyAttackSound.Play();
    }

    //Music 
    public void playForestMusic()
    {
        forestMusic.Play();
    }
    public void stopForestMusic()
    {
        forestMusic.Stop();
    }
    public void playCityMusic()
    {
        cityMusic.Play();
    }
    public void stopCityMusic()
    {
        cityMusic.Stop();
    }
    public void playSewersMusic()
    {
        sewersMusic.Play();
    }
    public void stopSewersMusic()
    {
        sewersMusic.Stop();
    }
    public void playHouseMusic()
    {
        houseMusic.Play();
    }
    public void stopHouseMusic()
    {
        houseMusic.Stop();
    }
    public void playAirMusic()
    {
        airMusic.Play();
    }
    public void stopAirMusic()
    {
        airMusic.Stop();
    }
    public void playBossMusic()
    {
        bossMusic.Play();   
    }
    public void stopBossMusic()
    {
        bossMusic.Stop();
    }
    public void playMainMenuMusic()
    {
        mainMenuMusic.Play();
    }
    public void StopMainMenuMusic()
    {
        mainMenuMusic.Stop();
    }

    public void playStartMusic()
    {
        startMusic.Play();
    }
    public void StopStartMusic()
    {
        startMusic.Stop();
    }
}
