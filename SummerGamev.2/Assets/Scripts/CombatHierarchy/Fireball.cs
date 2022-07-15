using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    public int damage = 3;
    public int size = 20;
    public float growthSpeed = 100f;
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
            Debug.Log(Time.deltaTime);
            this.transform.localScale = this.transform.localScale * 1.05f;
            yield return new WaitForSeconds(0.05f);
        }
        rigid.isKinematic = false;
        rigid.velocity = InitialVelocity;
    }
    private void OnCollisionEnter(Collision collision) {
        base.CollisionEnter(collision, damage);
    }
}
