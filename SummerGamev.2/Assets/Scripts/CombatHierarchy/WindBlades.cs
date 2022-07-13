using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlades : Projectile
{
    public WindBlades() : base("WindBlades", Type.DAMAGE, 3, 20f)
    {
        
    }
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        obj = this.gameObject;
        base.Start();
        Debug.Log(initialVelocity);
    }
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}