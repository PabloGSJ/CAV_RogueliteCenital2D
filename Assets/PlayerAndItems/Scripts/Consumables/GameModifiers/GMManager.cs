using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMManager : MonoBehaviour
{
    // CONTEXT:

    public PlayerStateMachine Player;

    // GMable variables
    private float dmgMod;
    private float speedMod;
    private bool enemiesDropBullets = true;
    private float numEnemiesMod;

    // GM specific functions
    public void ChangeDmgMod(float value)
    {
        dmgMod = value;
        Player.DmgMod = value;
    }

    public void ChangeSpeedMod (float value)
    {
        speedMod = value;
        Player.Speed = value;
    }

    public void ChangeEnemiesDropBullets(bool value)
    {
        enemiesDropBullets = value;
    }

    public void ChangeNumEnemiesMod(float value)
    {
        numEnemiesMod = value;
    }
}
