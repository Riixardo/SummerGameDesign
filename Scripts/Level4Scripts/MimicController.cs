using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CMF {
    public class MimicController : MonoBehaviour
    {
        public AudioSource mimicSFX;
        NavMeshAgent agent;
        Animator anim;
        public Transform player;
        private MimicState currentState;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            if(player == null) {
                player = GameObject.FindWithTag("Boss").GetComponent<BossController>().player;
            }
            currentState = new Idle(this.gameObject, anim, player, agent, mimicSFX);
        }
        void Update()
        {
            currentState = currentState.Process();
            CheckBattleWaitTime();
        }   
        public void CheckBattleWaitTime() {
            if(currentState is Battle) {
                var state = (Battle)currentState;
                if(state.StartWaitTime) {
                    StartCoroutine(((Battle)currentState).WaitBeforeIdle());
                    state.StartWaitTime = false;

                }
            }
        }
        public MimicState getState()
        {
            return currentState;
        }
    }
}