using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelingSystem : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToNextLvl;
    public float expReqPerLvlFactor = 1.1f;
    public TalentSystem talents;
    public TMP_Text levelText;
    private void Start()
    {
        levelText.text = "Level " + level;
    }
    public void EarnExperience(int exp)
    {
        currentExp += exp;
        CheckLevelUp();
    }
    void CheckLevelUp()
    {
        if(currentExp >= expToNextLvl)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        level++;
        levelText.text = "Level " + level;
        currentExp -= expToNextLvl;
        expToNextLvl = (int)(expToNextLvl * expReqPerLvlFactor);
        talents.AddTalentPoints(3);
        ////Code here to route new stats unlocked from level up to master stats manager
        ////It should be 3 hp, 1 magic damage, 1 phy damage, 1 mana (tentative)
        CheckLevelUp(); // Call recursively in case multiple levels are gained at once
    }
}