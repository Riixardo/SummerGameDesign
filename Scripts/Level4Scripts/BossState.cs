using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CMF
{
    public class BossState : MonoBehaviour
    {
        public enum STATE
        {
            IDLE, TELEPORT, ATTACK1, ATTACK2, SUMMONING, DEAD, ULTIMATE
        };
        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };
        public STATE name;
        protected EVENT stage;
        protected GameObject monster;
        protected Transform player;
        protected BossState nextState;
        protected NavMeshAgent agent;

        protected float visDis = 300.0f;
        protected float visAngle = 360.0f;
        protected float scytheSwingDis = 37.0f;
        protected float scytheBeamDis = 150f;

        protected Vector3 direction;
        protected float angleToPlayer;

        protected CoroutineHandler routineHandler;
        protected PlayerStatsManager stats;
        protected Enemies thisStats;

        public static bool halfway = false, mimicsDead = false, ultimateActivated = false, ultimateIsDone = false;
        protected float auraTime, auraInterval = 0.2f;

        protected Quaternion realStartRotation;
        protected Vector3 realStartPosition;
        public BossState(GameObject _monster, Transform _player, NavMeshAgent _agent)
        {
            monster = _monster;
            player = _player;
            agent = _agent;
            stage = EVENT.ENTER;
            if(halfway) {
                agent.speed = 12f;
            }
        }

        public virtual void Enter() 
        { 
            stage = EVENT.UPDATE; 
            routineHandler = GameObject.Find("CoHandler").GetComponent<CoroutineHandler>();
            stats = player.parent.GetComponent<CombatSystem>().stm;
            thisStats = monster.GetComponent<JumpEnemies>();
            Debug.Log(thisStats);
        }
        public virtual void Update() 
        { 
            RunHazardousAura();
            if(IsHalfway()) {
                if(name == STATE.ATTACK1 || name == STATE.ATTACK2) {
                    monster.transform.GetChild(1).transform.localPosition = realStartPosition;
                    monster.transform.GetChild(1).transform.localRotation = realStartRotation;
                }
                Debug.Log("PP");
                //player.parent.GetComponent<MusicPlayer>().playPhase2();
                Debug.Log("GG");
                nextState = new B_Summoning(monster, player, agent);
                stage = EVENT.EXIT;
            }
            if(halfway && checkUltimateAttack()) {
                if(name == STATE.ATTACK1 || name == STATE.ATTACK2) {
                    monster.transform.GetChild(1).transform.localPosition = realStartPosition;
                    monster.transform.GetChild(1).transform.localRotation = realStartRotation;
                }
                nextState = new B_Ultimate(monster, player, agent);
                stage = EVENT.EXIT;
            }
        }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public BossState Process()
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
        protected void StatsUpdate()
        {
            direction = player.position - monster.transform.position;
            angleToPlayer = Vector3.Angle(direction, agent.transform.forward);
        }
        protected bool IsHalfway() {
            if(thisStats.health < thisStats.maxHealth / 2 && !halfway) {
                halfway = true;
                return true;
            }
            return false;
        }
        protected bool checkUltimateAttack() {
            if(thisStats.health < thisStats.maxHealth * 0.2f && halfway && !ultimateActivated) {
                ultimateActivated = true;
                return true;
            }
            return false;
        }
        protected bool canSeePlayer()
        {
            if (direction.magnitude < visDis && angleToPlayer < visAngle)
            {
                return true;
            }
            return false;
        }
        protected bool canScytheSwingPlayer()
        {
            if (direction.magnitude < scytheSwingDis)
            {
                return true;
            }
            return false;
        }
        protected bool canScytheBeamPlayer()
        {
            if (direction.magnitude < scytheBeamDis)
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
        protected void FollowPlayer()
        {
            Quaternion rotateAngle = Quaternion.LookRotation(direction);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, rotateAngle, 2f * Time.deltaTime);
            agent.SetDestination(player.transform.position);
        }
        protected void RunHazardousAura() {
            if(direction.magnitude < 30f && Time.time > auraTime) {
                player.parent.GetComponent<CombatSystem>().onHit(1);
                auraTime = Time.time + auraInterval;
            }
        }
    }
    public class B_Teleport : BossState
    {
        bool runOnce = true, changeState;
        public B_Teleport(GameObject _monster, Transform _player, NavMeshAgent _agent) : base(_monster, _player, _agent)
        {
            name = STATE.TELEPORT;
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            if(runOnce) {
                monster.transform.position = new Vector3();
                routineHandler.StartTeleport();
                runOnce = false;
            }
            if(!runOnce && (monster.transform.position - player.position).magnitude < 100) {
                nextState = new B_Float(monster, player, agent);
                stage = EVENT.EXIT;
            }
            base.Update();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
    public class B_Float : BossState
    {
        float startAttackTime;
        float time;
        public B_Float(GameObject _monster, Transform _player, NavMeshAgent _agent) : base(_monster, _player, _agent)
        {
            name = STATE.IDLE;
            startAttackTime = halfway ? 1.5f: 2f;
        }
        public override void Enter()
        {
            agent.Resume();
            base.Enter();
        }
        public override void Update()
        {
            time += Time.deltaTime;
            StatsUpdate();
            if (canSeePlayer())
            {
                if(time > startAttackTime && canScytheSwingPlayer()) {
                    nextState = new B_ScytheSwing(monster, player, agent);
                    stage = EVENT.EXIT;
                    agent.Stop();
                }
                else if(time > startAttackTime && canScytheBeamPlayer()) {
                    nextState = new B_ScytheBeam(monster, player, agent);
                    stage = EVENT.EXIT;
                    agent.Stop();
                }
                agent.SetDestination(player.position);
            }
            else {
                var o = GameObject.Find("BossRespawnPoint");
                monster.transform.position = o.transform.position;
                monster.transform.rotation = o.transform.rotation;
            }
            base.Update();
        }
        public override void Exit()
        {
            agent.Stop();
            base.Exit();
        }
    }
    public class B_ScytheSwing : BossState
    {
        bool runOnce, goBackwards;
        float rotateAmount = 0.5f, totalAmount;
        Quaternion startRotation;
        public B_ScytheSwing(GameObject _monster, Transform _player, NavMeshAgent _agent) : base(_monster, _player, _agent)
        {
            name = STATE.ATTACK1;
        }
        public override void Enter()
        {
            agent.Stop();
            base.Enter();
        }
        public override void Update()
        {
            StatsUpdate();
            if(!runOnce) {
                realStartRotation = monster.transform.GetChild(1).transform.localRotation;
                realStartPosition = monster.transform.GetChild(1).transform.localPosition;
                var r = monster.transform.GetChild(1).transform.localPosition;
                var p = monster.transform.GetChild(1).transform.localRotation;
                monster.transform.GetChild(1).transform.localPosition = new Vector3(r.x, 17.36f, r.z);
                monster.transform.GetChild(1).transform.localRotation = p * Quaternion.Euler(45f, 0, 0);
                startRotation = monster.transform.GetChild(1).transform.localRotation;
                runOnce = true;
            }
            else {
                monster.transform.GetChild(1).transform.Rotate(0f, 0f, rotateAmount);
                if(totalAmount > 130f) {
                    rotateAmount = -rotateAmount;
                    goBackwards = true;
                }
                if(totalAmount < 0f) {
                    monster.transform.GetChild(1).transform.localPosition = realStartPosition;
                    monster.transform.GetChild(1).transform.localRotation = realStartRotation;
                    nextState = new B_Float(monster, player, agent);
                    stage = EVENT.EXIT;
                }
                totalAmount = totalAmount + rotateAmount;
            }
            Debug.DrawRay(monster.transform.GetChild(1).position, monster.transform.GetChild(1).up * 100, Color.green);
            Debug.DrawRay(monster.transform.GetChild(1).position, player.position - monster.transform.GetChild(1).position);
            base.Update();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
    public class B_ScytheBeam : BossState
    {
        bool runOnce, goBackwards, createBeams1 = true, createBeams2 = true, createBeams3 = true, createBeams4 = true;
        float rotateAmount = 1f, totalAmount, scytheSpeed = 60f;
        Quaternion startRotation;
        public B_ScytheBeam(GameObject _monster, Transform _player, NavMeshAgent _agent) : base(_monster, _player, _agent)
        {
            name = STATE.ATTACK2;
        }
        public override void Enter()
        {
            agent.Stop();
            base.Enter();
        }
        public override void Update()
        {
            StatsUpdate();
            if(!runOnce) {
                realStartRotation = monster.transform.GetChild(1).transform.localRotation;
                realStartPosition = monster.transform.GetChild(1).transform.localPosition;
                var r = monster.transform.GetChild(1).transform.localPosition;
                var p = monster.transform.GetChild(1).transform.localRotation;
                monster.transform.GetChild(1).transform.localPosition = new Vector3(r.x, 17.36f, r.z);
                monster.transform.GetChild(1).transform.localRotation = p * Quaternion.Euler(90f, 0, 0);
                startRotation = monster.transform.GetChild(1).transform.localRotation;
                runOnce = true;
            }
            else {
                monster.transform.GetChild(1).transform.Rotate(0f, 0f, rotateAmount);
                if(totalAmount > 180f) {
                    rotateAmount = -rotateAmount;
                    goBackwards = true;
                }
                if(totalAmount < 0f) {
                    monster.transform.GetChild(1).transform.localPosition = realStartPosition;
                    monster.transform.GetChild(1).transform.localRotation = realStartRotation;
                    nextState = new B_Float(monster, player, agent);
                    stage = EVENT.EXIT;
                }
                if(totalAmount > 20f && createBeams1 && !goBackwards) {
                    CreateBeam();
                    createBeams1 = false;             
                }
                if(totalAmount > 60f && createBeams2 && !goBackwards) {
                    CreateBeam();
                    createBeams2 = false;             
                }
                if(totalAmount > 100f && createBeams3 && !goBackwards) {
                    CreateBeam();
                    createBeams3 = false;             
                }
                if(totalAmount > 140f && createBeams4 && !goBackwards && halfway) {
                    CreateBeam();
                    createBeams4 = false;
                }
                totalAmount = totalAmount + rotateAmount;
            }
            Debug.DrawRay(monster.transform.position, direction, Color.blue);
            Debug.DrawRay(monster.transform.GetChild(1).position, monster.transform.GetChild(1).up * 100, Color.green);
            Debug.DrawRay(monster.transform.GetChild(1).position, player.position - monster.transform.GetChild(1).position);
            base.Update();
        }
        public override void Exit()
        {
            base.Exit();
        }
        void CreateBeam() {
            var o = Instantiate(monster.GetComponent<BossController>().objects[0], monster.transform.GetChild(1).GetChild(0).position, monster.transform.GetChild(1).rotation);
            o.GetComponent<Rigidbody>().velocity = (player.position - monster.transform.GetChild(1).GetChild(0).position).normalized * scytheSpeed;
            Destroy(o, 5f);
        }
    }
    public class B_Summoning : BossState
    {
        float time;
        GameObject altar, circle;
        bool runOnce = true, changeState, waitIsOver, isDone;
        public B_Summoning(GameObject _monster, Transform _player, NavMeshAgent _agent) : base(_monster, _player, _agent)
        {
            name = STATE.SUMMONING;
            altar = GameObject.Find("HerobrineAltar");
        }
        public override void Enter()
        {
            agent.Stop();
            base.Enter();
        }
        public override void Update()
        {
            time += Time.deltaTime;
            if(time > 1.5f) waitIsOver = true;
            if(waitIsOver) thisStats.canBeAttacked = false;
            if(waitIsOver && !isDone) {
                thisStats.canBeAttacked = false;
                monster.transform.position = altar.transform.position;
                monster.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                circle = Instantiate(monster.GetComponent<BossController>().objects[1], altar.transform.position + new Vector3(0f, -1f, 0f), new Quaternion());
                isDone = true;
            }
            if(isDone && mimicsDead) {
                thisStats.canBeAttacked = true;
                nextState = new B_Float(monster, player, agent);
                stage = EVENT.EXIT;
            }
            RunHazardousAura();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
    public class B_Ultimate : BossState {
        bool isDone;
        float time;
        GameObject altar, circle;
        public B_Ultimate(GameObject _monster, Transform _player, NavMeshAgent _agent) : base(_monster, _player, _agent)
        {
            name = STATE.ULTIMATE;
            altar = GameObject.Find("HerobrineAltar");
        }
        public override void Enter()
        {
            agent.Stop();
            base.Enter();
        }
        public override void Update()
        {
            time += Time.deltaTime;
            if(time > 1.5f) thisStats.canBeAttacked = false;
            if(time > 1.5f && !isDone) {
                thisStats.canBeAttacked = false;
                monster.transform.position = altar.transform.position;
                monster.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                circle = Instantiate(monster.GetComponent<BossController>().objects[2], altar.transform.position + new Vector3(0f, 100f, 0f), new Quaternion());
                isDone = true;
                Debug.Log("lol");
            }
            if(ultimateIsDone) {
                thisStats.canBeAttacked = true;
                nextState = new B_Float(monster, player, agent);
                stage = EVENT.EXIT;
            }
            RunHazardousAura();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}