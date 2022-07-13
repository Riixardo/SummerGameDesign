using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentSystem : MonoBehaviour
{
    // Talent Point Management

    public int numTalentPoints;
    int maxTalentPoints;


    public void AddTalentPoint()
    {
        AddTalentPoints(1);
    }

    public void AddTalentPoints(int points)
    {
        numTalentPoints += points;
        maxTalentPoints += points;
    }

    public bool HaveTalentPoints()
    {
        return numTalentPoints > 0;
    }

    public int GetNumTalentPoints()
    {
        return numTalentPoints;
    }

    public void SpendTalentPoints(int num)
    {
        numTalentPoints -= num;
        if(numTalentPoints < 0)
        {
            Debug.Log("Issue spending more talent points than you hold!");
        }
        UpdateTalentUIData();
    }




    // Talent Skill Tree Code

    // UI Components
    public TMP_Text numTalentsText;
    public TMP_Text speedUpText;
    public TMP_Text jumpsUpText;
    public TMP_Text hpUpText;

    public Button speedUpButton;
    public Button jumpsUpButton;
    public Button hpUpButton;

    // Talent data
    string speedUpBaseText = "Speed increase: ";
    string jumpsUpBaseText = "Num jumps: ";
    string hpUpBaseText = "Total hp increase: ";

    int speedTalentProgressionState;
    int jumpTalentProgressionState;
    int hpTalentProgressionState;

    float[] speedTalentUpgrades = { 1.2f, 1.4f, 1.6f};
    int[] jumpTalentUpgrades = { 2, 3, 4 };
    float[] hpTalentUpgrades = { 1.1f, 1.2f, 1.3f };

    // Access to player data


    private void OnEnable()
    {
        UpdateTalentUIData();
    }

    public void UpdateTalentUIData()
    {
        SetTalentInfoCards();
        SetButtonInteractability();
        
        // Num talent point UI update
        numTalentsText.text = "Current Points: " + numTalentPoints;
    }

    // Reset UI text to base states
    void SetTalentInfoCards()
    {
        speedUpText.text = "Current cost: " + (speedTalentProgressionState+1) + "\n" + speedUpBaseText + (speedTalentProgressionState * 20) + "%!";
        Debug.Log("Speed: " + speedUpText.text);
        jumpsUpText.text = "Current cost: " + (jumpTalentProgressionState + 1) + "\n" + jumpsUpBaseText + (jumpTalentProgressionState + 1);
        Debug.Log("Jumps: " + jumpsUpText.text);
        hpUpText.text = "Current cost: " + (hpTalentProgressionState + 1) + "\n" + hpUpBaseText + (hpTalentProgressionState * 10) + "%!";
        Debug.Log("HP: " + hpUpText.text);
        hpUpText.text = hpUpBaseText + "0";
        jumpsUpText.text = jumpsUpBaseText + "1";
    }

    void SetButtonInteractability()
    {
        if(numTalentPoints < speedTalentProgressionState+1 || !(speedTalentProgressionState < speedTalentUpgrades.Length))
        {
            speedUpButton.interactable = false;
        }
        else
        {
            speedUpButton.interactable = true;
        }
        if(numTalentPoints < jumpTalentProgressionState+1 || !(jumpTalentProgressionState < jumpTalentUpgrades.Length))
        {
            jumpsUpButton.interactable = false;
        }
        else
        {
            jumpsUpButton.interactable = true;
        }
        if(numTalentPoints < hpTalentProgressionState+1 || !(hpTalentProgressionState < hpTalentUpgrades.Length))
        {
            hpUpButton.interactable = false;
        }
        else
        {
            hpUpButton.interactable = true;
        }
    }

    public void PurchaseSpeedUpgrade()
    {
        numTalentPoints -= speedTalentProgressionState + 1;
        speedTalentProgressionState++;
        UpdateTalentUIData();
    }

    public void PurchaseJumpUpgrade()
    {
        numTalentPoints -= jumpTalentProgressionState + 1;
        jumpTalentProgressionState++;
        UpdateTalentUIData();
    }

    public void PurchaseHPUpgrade()
    {
        numTalentPoints -= hpTalentProgressionState + 1;
        hpTalentProgressionState++;
        UpdateTalentUIData();
    }


    public void ResetTalentPoints()
    {
        numTalentPoints = maxTalentPoints;
        UpdateTalentUIData();
        ResetProgressionStates();
        SetTalentInfoCards();
    }

    void ResetProgressionStates()
    {
        speedTalentProgressionState = 0;
        hpTalentProgressionState = 0;
        jumpTalentProgressionState = 0;
    }


}
