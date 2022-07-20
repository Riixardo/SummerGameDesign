using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    // Core stat tracking
    public int maxHealth, health, lives, currencyTotal, playerLevel, magicDamage, physicalDamage, currentTalentPointsHeld, mana, maxMana, defense;
    
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
            defense = stats.defense;
        }      
    }
}
