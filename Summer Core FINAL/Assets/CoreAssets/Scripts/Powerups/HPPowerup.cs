using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CMF
{
    public class HPPowerup: MonoBehaviour
    {
        public int addedHP = 3;
        public PlayerStatsManager stm;
        // Start is called before the first frame update
        void Start()
        {
            stm = GameObject.Find("StatManager").GetComponent<PlayerStatsManager>();
        }

        // Update is called once per frame

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("player in range");
            if (other.gameObject.GetComponent<AdvancedWalkerController>())
            {
                
                stm.health += addedHP;
                stm.maxHealth += addedHP;
                Destroy(gameObject);
            }
        }
    }
}
