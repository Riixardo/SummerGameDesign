using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : Damage
{
    public int Damage = 10;
    GameObject player, talentUI;
    public LightningStrike() : base("Lightning", Type.MAGIC) 
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        talentUI = player.GetComponent<CombatController>().GetTalentSystem();
        Damage += talentUI.GetComponent<TalentSystem>().magicDmgProgressionState;
        Destroy(gameObject, 1f);
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("LOLOLOL");
        if(other.GetComponent<TakeDamage>()) {
            other.GetComponent<TakeDamage>().LowerHealth(Damage);
        }
    }
}
