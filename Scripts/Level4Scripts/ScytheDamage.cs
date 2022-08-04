using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class ScytheDamage : MonoBehaviour
{
    public int Damage;
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponent<CombatSystem>().onHit(Damage);
        }
    }
}
