using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    public int damage = 3;
    public int size = 50;
    public Fireball() : base("Fireball", Type.MAGIC, 20f) 
    { 
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigid = this.GetComponent<Rigidbody>();
        base.Start();
        rigid.isKinematic = true;
        Debug.Log(InitialVelocity);
        StartCoroutine(Grow());
    }
    void Update()
    {
        
    }
    IEnumerator Grow()
    {
        for(int i = 0; i < size; i++)
        {
            this.transform.localScale = this.transform.localScale * 1.01f;
            yield return null;
        }
        rigid.isKinematic = false;
        rigid.velocity = InitialVelocity;
    }
    private void OnCollisionEnter(Collision collision) {
        base.CollisionEnter(collision, damage);
    }
}
