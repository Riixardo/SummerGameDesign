using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int health;
    void Update()
    {
        if(health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    public void LowerHealth(int damage)
    {
        health -= damage;
    }
}
