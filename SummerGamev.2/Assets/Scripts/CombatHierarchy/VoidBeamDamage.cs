using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBeamDamage : MonoBehaviour
{
    public int Damage = 12;
    GameObject player, talentUI;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        talentUI = player.GetComponent<CombatController>().GetTalentSystem();
        Damage += talentUI.GetComponent<TalentSystem>().magicDmgProgressionState;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TakeDamage>())
        {
            other.GetComponent<TakeDamage>().LowerHealth(Damage);
        }
    }
}
