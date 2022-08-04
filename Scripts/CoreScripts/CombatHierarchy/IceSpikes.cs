using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    public class IceSpikes : MonoBehaviour
    {
        Rigidbody rigid;
        float time = 0f;
        public float Damage = 5f;
        public IceSpikes()
        {
        }
        void Start()
        {
            rigid = this.GetComponent<Rigidbody>();
            rigid.isKinematic = true;
        }
        void Update()
        {
            time = time + Time.deltaTime;
            if (time >= 1f && rigid.isKinematic)
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Enemies>())
            {
                other.gameObject.GetComponent<Enemies>().onHit(Damage);
            }
        }
    }
}
