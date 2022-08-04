using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CMF
{
    public class BossController : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;
        public Transform player;
        public GameObject[] objects;
        private BossState currentState;
        private GameObject copy;
        void Start()
        {
            copy = GameObject.Find("STONECOPY");
            player = GameObject.Find("ModelRoot").GetComponent<Transform>();
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            currentState = new B_Teleport(this.gameObject, player, agent);
            gameObject.SetActive(false);
        }
        void Update()
        {
            if(checkIfStart())
            {
                anim.SetBool("NotStill", true);
                currentState = currentState.Process();
            }
        }
        bool checkIfStart()
        {
            if (!copy.active)
            {
                return true;
            }
            return false;
        }
        public BossState getState()
        {
            return currentState;
        }
    }
}