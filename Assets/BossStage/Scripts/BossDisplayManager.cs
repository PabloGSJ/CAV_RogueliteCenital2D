using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDisplayManager : MonoBehaviour
{
    // Displays
    public Text BossHealthPointsDisplay;

    public void DisplayNewBossHealth(int newHealth)
    {
        BossHealthPointsDisplay.text = newHealth.ToString();
    }
}
