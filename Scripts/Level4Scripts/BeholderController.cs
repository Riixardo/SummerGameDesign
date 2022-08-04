using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CMF {
    public class BeholderController : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;
        public Transform player;
        private BeholderState currentState;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            currentState = new _Idle(this.gameObject, anim, player, agent);
        }
        void Update()
        {
            currentState = currentState.Process();
            CheckBattleWaitTime();
        }   
        public void CheckBattleWaitTime() {
            if(currentState is _Battle) {
                var state = (_Battle)currentState;
                if(state.StartWaitTime) {
                    StartCoroutine(((_Battle)currentState).WaitBeforeIdle());
                    state.StartWaitTime = false;

                }
            }
        }
        public BeholderState getState()
        {
            return currentState;
        }
    }
}