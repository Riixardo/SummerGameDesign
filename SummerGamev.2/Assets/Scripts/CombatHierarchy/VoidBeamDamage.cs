using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBeamDamage : MonoBehaviour
{
    public int damage = 12;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TakeDamage>())
        {
            other.GetComponent<TakeDamage>().LowerHealth(damage);
        }
    }
}
