using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    Melee meleeController;
    Type type = Type.MELEE;
    public int damage;
    void Start() {
        meleeController = GetComponentInParent<Melee>();
    }
    void OnCollisionEnter(Collision c) {
        if(meleeController.isSlashing) {
            if(c.gameObject.GetComponent<TakeDamage>()) {
            c.gameObject.GetComponent<TakeDamage>().LowerHealth(damage);
            }
        }
    }
    void OnCollisionExit(Collision c) {
        if(meleeController.isSlashing) {
            if(c.gameObject.GetComponent<TakeDamage>()) {
            c.gameObject.GetComponent<TakeDamage>().LowerHealth(damage);
            }
        }
    }
}
