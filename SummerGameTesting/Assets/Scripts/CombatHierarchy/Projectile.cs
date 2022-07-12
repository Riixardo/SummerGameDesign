using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Projectile : Damage
    {
        public int enemyLayer = 8;
        private int bounceTimes = 0;
        public int maxBounceTimes = 5;

        protected Vector3 initialVelocity; 

        public Projectile(string n, Type type, int dmg) : base(n, type, dmg) 
        {
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
