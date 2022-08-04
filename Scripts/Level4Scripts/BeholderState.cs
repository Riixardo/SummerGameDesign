using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CMF {
    public class BeholderState
    {
        public enum STATE 
        {
            IDLE, BATTLE, ATTACK1, ATTACK2, RUN, DEAD
        };
        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };
        public STATE name;
        protected EVENT stage;
        protected GameObject monster;
        protected Animator anim;
        protected Transform player;
        protected BeholderState nextState;
        protected NavMeshAgent agent;

        protected float visDis = 17.0f;
        protected float visAngle = 360.0f;
        protected float attackDis = 5.0f;

        protected Vector3 direction;
        protected float angleToPlayer;
        
        public BeholderState(GameObject _monster, Animator _anim, Transform _player, NavMeshAgent _agent)
        {
            monster = _monster;
            anim = _anim;
            player = _player;
            agent = _agent;
            stage = EVENT.ENTER;
        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public BeholderState Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }
            return this;
        }
        protected void StatsUpdate() {
            direction = player.position - agent.transform.position;
            angleToPlayer = Vector3.Angle(direction, agent.transform.forward);
        }
        protected bool canSeePlayer()
        {
            if (direction.magnitude < visDis && angleToPlayer < visAngle)
            {
                return true;
            }
            return false;
        }
        protected bool canAttackPlayer()
        {
            if(direction.magnitude < attackDis)
            {
                return true;
            }
            return false;
        }
        public bool isNearPlayer()
        {
            if (direction.magnitude < visDis + 3f)
            {
                return true;
            }
            return false;
        }
        protected void FollowPlayer() {
            Quaternion rotateAngle = Quaternion.LookRotation(direction);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, rotateAngle, 2f * Time.deltaTime);
            agent.SetDestination(player.transform.position);
        }
    }
    public class _Idle: BeholderState
    {
        public _Idle(GameObject _monster, Animator _anim, Transform _player, NavMeshAgent _agent) : base(_monster, _anim, _player, _agent)
        {
            name = STATE.IDLE;
        }
        public override void Enter()
        {
            anim.SetTrigger("ReturnIdle");
            base.Enter();
        }
        public override void Update()
        {
            StatsUpdate();
            if (canSeePlayer())
            {
                nextState = new _Battle(monster, anim, player, agent);
                stage = EVENT.EXIT;
            }
        }
        public override void Exit()
        {
            anim.ResetTrigger("ReturnIdle");
            base.Exit();
        }
    }
    public class _Battle: BeholderState
    {
        public bool StartWaitTime;
        private bool OneShot = true;
        public _Battle(GameObject _monster, Animator _anim, Transform _player, NavMeshAgent _agent) : base(_monster, _anim, _player, _agent)
        {
            name = STATE.BATTLE;
        }
        public override void Enter()
        {
            agent.updatePosition = false;
            anim.SetTrigger("ReturnIdleBattle");
            base.Enter();
        }
        public override void Update()
        {
            StatsUpdate();
            if(canSeePlayer())
            {
                nextState = new _Run(monster, anim, player, agent);
                stage = EVENT.EXIT;
            }
            else if(!StartWaitTime && OneShot) {
                StartWaitTime = true;
                OneShot = false;
            }
        }
        public override void Exit()
        {
            //anim.ResetTrigger("ReturnIdleBattle");
            base.Exit();
        }
        public IEnumerator WaitBeforeIdle() {
            Debug.Log("Triggered");
            yield return new WaitForSeconds(3f);
            if(nextState == null) {
                nextState = new _Idle(monster, anim, player, agent);
                stage = EVENT.EXIT;
            }
        }
    }
    public class _Run : BeholderState
    {
        private float runSpeed = 7f;

        public _Run(GameObject _monster, Animator _anim, Transform _player, NavMeshAgent _agent) : base(_monster, _anim, _player, _agent)
        {
            name = STATE.RUN;
            agent.speed = runSpeed;
        }
        public override void Enter()
        {
            agent.updatePosition = true;
            anim.SetTrigger("Run");
            base.Enter();
        }
        public override void Update()
        {
            StatsUpdate();
            if (isNearPlayer())
            {
                if (canAttackPlayer())
                {
                    nextState = new _Attack1(monster, anim, player, agent);
                    stage = EVENT.EXIT;
                }
                FollowPlayer();
            }
            else
            {
                agent.updatePosition = false;
                nextState = new _Battle(monster, anim, player, agent);
                stage = EVENT.EXIT;
            }
        }
        public override void Exit()
        {
            anim.ResetTrigger("Run");
            base.Exit();
        }
    }
    public class _Attack1: BeholderState
    {
        bool attackOnce;
        float time = 0f, attackTime;
        int damage = 3;
        public _Attack1(GameObject _monster, Animator _anim, Transform _player, NavMeshAgent _agent) : base(_monster, _anim, _player, _agent)
        {
            name = STATE.ATTACK1;
        }
        public override void Enter()
        {
            anim.SetBool("Attack1", true);
            base.Enter();
        }
        public override void Update()
        {
            Debug.Log(attackTime);
            time += Time.deltaTime;
            StatsUpdate();
            if (canAttackPlayer()) {
                if(time >= 1.0f ) {
                    nextState = new _Attack2(monster, anim, player, agent);
                    stage = EVENT.EXIT;
                    anim.SetBool("Attack1", false);
                }
                if(time >= attackTime) {
                    player.GetComponent<CombatSystem>().onHit(damage);
                    attackTime = 100f;
                    attackOnce = true;
                }
                else if(!attackOnce){
                    attackTime = anim.GetCurrentAnimatorStateInfo(0).length / 2f;
                }
                FollowPlayer();
            }
            else {
                nextState = new _Run(monster, anim, player, agent);
                stage = EVENT.EXIT;
            }
        }
        public override void Exit()
        {
            anim.SetBool("Attack1", false);
            base.Exit();
        }
    }
    public class _Attack2: BeholderState
    {
        bool attackOnce;
        float time = 0f, attackTime;
        int damage = 4;
        public _Attack2(GameObject _monster, Animator _anim, Transform _player, NavMeshAgent _agent) : base(_monster, _anim, _player, _agent)
        {
            Debug.Log("2nd");
            name = STATE.ATTACK2;
        }
        public override void Enter()
        {
            anim.SetBool("Attack2", true);
            base.Enter();
        }
        public override void Update()
        {
            time += Time.deltaTime;
            StatsUpdate();
            if (canAttackPlayer()) {
                if(time >= 1.0f ) {
                    nextState = new _Attack1(monster, anim, player, agent);
                    stage = EVENT.EXIT;
                    anim.SetBool("Attack2", false);
                }
                if(time >= attackTime) {
                    player.GetComponent<CombatSystem>().onHit(damage);
                    attackTime = 100f;
                    attackOnce = true;
                }
                else if(!attackOnce){
                    attackTime = anim.GetCurrentAnimatorStateInfo(0).length / 2f;
                }
                FollowPlayer();
            }
            else {
                nextState = new _Run(monster, anim, player, agent);
                stage = EVENT.EXIT;
            }
        }
        public override void Exit()
        {
            anim.SetBool("Attack2", false);
            base.Exit();
        }
    }
}
