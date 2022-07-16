using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damage
{
    public int Damage;
    private int bounceTimes = 0;
    public int maxBounceTimes = 1;
    public float speed;
    protected Rigidbody rigid;
    protected GameObject cam;
    public Vector3 InitialVelocity;

    public Projectile(string n, Type type) : base(n, type) 
    {
        this.speed = speed;
    }
    protected void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigid = this.GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("ModelRoot");
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        if(!(type == Type.MINION)) {
            InitialVelocity = cam.transform.forward * speed;
            rigid.velocity = InitialVelocity;
        }
        Destroy(gameObject, 5f);
    }
    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if(collision.gameObject.GetComponent<TakeDamage>())
        {
            collision.gameObject.GetComponent<TakeDamage>().LowerHealth(Damage);
        }
        if (name != "WindBlades") {
            rigid.useGravity = true;
        }
        bounceTimes++;
        if(bounceTimes >= maxBounceTimes)
        {
            Destroy(gameObject);
        }
    }
}
