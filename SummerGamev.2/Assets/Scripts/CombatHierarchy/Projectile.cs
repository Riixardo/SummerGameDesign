using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damage
{
    public int enemyLayer = 8;
    private int bounceTimes = 0;
    public int maxBounceTimes = 2;
    protected float speed;
    protected GameObject cam;
    public Vector3 initialVelocity;

    public Projectile(string n, Type type, int dmg, float speed) : base(n, type, dmg) 
    {
        this.speed = speed;
    }
    protected void Start()
    {
        cam = GameObject.FindWithTag("ModelRoot");
        initialVelocity = cam.transform.forward * speed;
        rigid.velocity = initialVelocity;
        Destroy(obj, 5f);
    }
    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<TakeDamage>())
        {
            collision.gameObject.GetComponent<TakeDamage>().LowerHealth(damage);
            Destroy(gameObject);
        }
        bounceTimes++;
        if (bounceTimes >= maxBounceTimes)
        {
            Destroy(obj);
        }
    }
}
