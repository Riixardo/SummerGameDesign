using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Projectile : Damage
    {
        public int enemyLayer = 8;
        private int bounceTimes = 0;
        public int maxBounceTimes = 1;

        public Vector3 initialVelocity = new Vector3(0f, 22f, 0f);

        public Projectile(string n, Type type, int dmg) : base(n, type, dmg) 
        {
        }
        protected void OnCollisionEnter(Collision collision)
        {

            Debug.Log(collision.gameObject);

            /*if (collision.gameObject.GetComponent<Transform>())
            {
                collision.gameObject.GetComponent<Transform>().health -= damage;
                Destroy(gameObject);

            }*/
            bounceTimes++;
            if (bounceTimes >= maxBounceTimes)
            {
                Destroy(o);
            }
        }
    }
