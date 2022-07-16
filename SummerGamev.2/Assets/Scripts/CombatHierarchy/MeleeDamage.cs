using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    Melee meleeController;
    Type type = Type.MELEE;
    public int Damage;
    GameObject player, talentUI;
    void Start() {
        meleeController = GetComponentInParent<Melee>();
        player = GameObject.FindWithTag("Player");
        talentUI = player.GetComponent<CombatController>().GetTalentSystem();
        Damage += talentUI.GetComponent<TalentSystem>().phyDmgProgressionState;
    }
    void OnCollisionEnter(Collision c) {
        if(meleeController.isSlashing) {
            if(c.gameObject.GetComponent<TakeDamage>()) {
            c.gameObject.GetComponent<TakeDamage>().LowerHealth(Damage);
            }
        }
    }
    void OnCollisionExit(Collision c) {
        if(meleeController.isSlashing && c.gameObject.tag != "WindBlade") {
            if(c.gameObject.GetComponent<TakeDamage>()) {
            c.gameObject.GetComponent<TakeDamage>().LowerHealth(Damage);
            }
        }
    }
}
