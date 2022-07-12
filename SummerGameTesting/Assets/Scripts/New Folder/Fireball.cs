using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    public Fireball() : base("Fireball", 4) {

    }
    void Start()
    {
        r = this.GetComponent<Rigidbody>();
        o = this.gameObject;
        r.velocity = initialVelocity;
        Destroy(o, 33f);
        Debug.Log("WD");
        Update();
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
    }
}
