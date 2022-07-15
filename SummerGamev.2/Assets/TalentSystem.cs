using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentSystem : MonoBehaviour
{
    public int numTalentPoints;
    int maxTalentPoints;
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
    public TMP_Text manaText;
    public TMP_Text magicDmgText;
    public TMP_Text phyDmgText;
    public TMP_Text hpText;

    public Button manaUpButton;
    public Button magicDmgButton;
    public Button phyDmgButton;
    public Button hpUpButton;

    // Talent data
    string speedUpBaseText = "Speed increase: ";
    string jumpsUpBaseText = "Num jumps: ";
    string hpUpBaseText = "Total hp increase: ";

    public int manaProgressionState;
    public int magicDmgProgressionState;
    public int phyDmgProgressionState;
    public int hpTalentProgressionState;

    private void OnEnable()
    {
        UpdateTalentUIData();
    }
    public void UpdateTalentUIData()
    {
        SetTalentInfoCards();
        SetButtonInteractability();
    }
    // Reset UI text to base states
    void SetTalentInfoCards()
    {
        numTalentsText.text = "" + numTalentPoints;
        manaText.text = "" + manaProgressionState;
        magicDmgText.text = "" + magicDmgProgressionState;
        phyDmgText.text = "" + phyDmgProgressionState;
        hpText.text = "" + hpTalentProgressionState;
    }
    void SetButtonInteractability()
    {
        if(numTalentPoints < 1)
        {
            manaUpButton.interactable = false;
        }
        else
        {
            manaUpButton.interactable = true;
        }
        if(numTalentPoints < 1)
        {
            magicDmgButton.interactable = false;
        }
        else
        {
            magicDmgButton.interactable = true;
        }
        if(numTalentPoints < 1)
        {
            phyDmgButton.interactable = false;
        }
        else
        {
            phyDmgButton.interactable = true;
        }
        if (numTalentPoints < 1)
        {
            hpUpButton.interactable = false;
        }
        else
        {
            hpUpButton.interactable = true;
        }
    }
    public void PurchaseManaUpgrade()
    {
        //numTalentPoints -= speedTalentProgressionState + 1;
        numTalentPoints--;
        manaProgressionState++;
        UpdateTalentUIData();
    }
    public void PurchaseMagicUpgrade()
    {
        //numTalentPoints -= jumpTalentProgressionState + 1;
        numTalentPoints--;
        magicDmgProgressionState++;
        UpdateTalentUIData();
    }
    public void PurchasePhysicalUpgrade()
    {
        //numTalentPoints -= hpTalentProgressionState + 1;
        numTalentPoints--;
        phyDmgProgressionState++;
        UpdateTalentUIData();
    }
    public void PurchaseHPUpgrade()
    {
        //numTalentPoints -= hpTalentProgressionState + 1;
        numTalentPoints--;
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
        manaProgressionState = 0;
        magicDmgProgressionState = 0;
        phyDmgProgressionState = 0;
        hpTalentProgressionState = 0;
    }
}
