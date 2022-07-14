using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenBeam : Damage
{
    private int damage = 50;
    public FallenBeam() : base("FallenBeam", Type.MAGIC)
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 1.4f);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("LOLOLOL");
        if (other.GetComponent<TakeDamage>())
        {
            other.GetComponent<TakeDamage>().LowerHealth(damage);
        }
    }
}
