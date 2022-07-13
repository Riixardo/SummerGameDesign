using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    // Experience stat tracking
    public int level;
    public int currentExp;
    public int expToNextLvl;
    public float expReqPerLvlFactor = 1.1f;

    // Talent point management
    public TalentSystem talents;

    private void Start()
    {
        talents = GetComponent<TalentSystem>();
    }

    public void EarnExperience(int exp)
    {
        currentExp += exp;
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        if(currentExp > expToNextLvl)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentExp -= expToNextLvl;
        expToNextLvl = (int)(expToNextLvl * expReqPerLvlFactor);

        talents.AddTalentPoint();

        CheckLevelUp(); // Call recursively in case multiple levels are gained at once
    }

}