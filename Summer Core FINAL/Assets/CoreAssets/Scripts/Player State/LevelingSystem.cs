using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingSystem : MonoBehaviour
{
    float time;
    int frames;
    //public TMP_Text fps;
    public int level;
    public int currentExp;
    public int expToNextLvl;
    public float expReqPerLvlFactor = 1.1f;
    public TalentSystem talents;
    public TMP_Text levelText;

    public Image expBar;

    private void Start()
    {
        levelText.text = "Level " + level;
        UpdateExpBar();
    }
    void Update() {
        time += Time.deltaTime;
        frames++;
        if(time >= 1.000f) {
            //fps.text = frames.ToString();
            time = 0f;
            frames = 0;
        }
    }
    public void EarnExperience(int exp)
    {
        currentExp += exp;
        UpdateExpBar();
        CheckLevelUp();
    }

    void UpdateExpBar()
    {
        float fillPercentage = (float)currentExp / (float)expToNextLvl;
        //Debug.Log("Fill %: " + fillPercentage);
        expBar.fillAmount = fillPercentage;
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
        UpdateExpBar();
        CheckLevelUp(); // Call recursively in case multiple levels are gained at once
    }
}