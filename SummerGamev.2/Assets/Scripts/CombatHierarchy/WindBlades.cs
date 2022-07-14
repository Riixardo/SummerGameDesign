using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlades : Projectile
{
    public int damage = 3;
    public WindBlades() : base("WindBlades", Type.MAGIC, 20f)
    {  
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        rigid = this.GetComponent<Rigidbody>();
        base.Start();
    }
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "WindBlade")
        {
            base.CollisionEnter(collision, damage);
        }
    }
}