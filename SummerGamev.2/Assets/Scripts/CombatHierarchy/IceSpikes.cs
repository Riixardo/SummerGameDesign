using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikes : Projectile
{
    float time = 0f;
    public IceSpikes() : base("IceSpikes", Type.MAGIC, 1, 30f) 
    {
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigid = this.GetComponent<Rigidbody>();
        obj = this.gameObject;
        base.Start();
        rigid.isKinematic = true;
        Debug.Log(InitialVelocity);
    }
    void Update()
    {
        time = time + Time.deltaTime;
        if(time >= 1f) {
            rigid.isKinematic = false;
            rigid.velocity = InitialVelocity;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
    }
}
