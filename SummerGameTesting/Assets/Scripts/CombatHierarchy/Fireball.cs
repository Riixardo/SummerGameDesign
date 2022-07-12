using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{


    public Fireball() : base("Fireball", Type.DAMAGE, 3) {
    }
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        obj = this.gameObject;
        initialVelocity = new Vector3(this.transform.forward);
        rigid.velocity = initialVelocity;
        Destroy(obj, 10f);
        Debug.Log("WD");
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
    }
}
