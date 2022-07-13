using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlades : Projectile
{
    public WindBlades() : base("WindBlades", Type.MAGIC, 3, 20f)
    {  
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        rigid = this.GetComponent<Rigidbody>();
        obj = this.gameObject;
        base.Start();
        Debug.Log(InitialVelocity);
    }
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}