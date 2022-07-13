using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : Damage
{
    private int damage = 10;
    private int iteration = 0;
    public LightningStrike() : base("Lightning", Type.MAGIC) 
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 1f);
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("LOLOLOL");
        if(other.GetComponent<TakeDamage>()) {
            other.GetComponent<TakeDamage>().LowerHealth(damage);
        }
    }
}
