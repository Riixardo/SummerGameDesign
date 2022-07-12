using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    // Core stat tracking
    public int maxHealth;
    public int health;
    public int lives;

    // Collectable stat tracking
    public int currencyTotal;

    // Experience stat tracking
    public int playerLevel;

    // Talent stat tracking
    public int currentTalentPointsHeld;

    public void UpdateViewerStats(PlayerStats stats)
    {
        if(stats != null)
        {
            maxHealth = stats.maxHealth;
            health = stats.health;
            lives = stats.lives;
            currencyTotal = stats.currencyTotal;
            playerLevel = stats.playerLevel;
            currentTalentPointsHeld = stats.currentTalentPointsHeld;
        }      
    }

}
