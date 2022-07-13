using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    public int damage = 3;
    public Fireball() : base("Fireball", Type.MAGIC, 20f) 
    { 
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigid = this.GetComponent<Rigidbody>();
        base.Start();
        Debug.Log(InitialVelocity);
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        base.CollisionEnter(collision, damage);
    }
}
