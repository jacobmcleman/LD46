using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShip))]
public class AIAttackState : MonoBehaviour, IWieldable
{
//    public enum EnemyState {Patrol, Chase, Attack}
//    public EnemyState currentState;
    public Transform whalePosition;
    public Transform playerPosition;
    public float fireDistance = 100f;
    public float fireAngle = 30f;

    private Teams _team = Teams.enemyTeam;

    public Teams team 
    { 
        get { return _team; } 
        set 
        {
            _team = value;
        }

    }

    private enum AIState
    {
        AttackingWhale,
        ChasingPlayer,
        RunningFromPlayer,
        AttackingEnemy,
        ChasingEnemy
    }
    private AIState curState;

    public float attackRange = 200f;
    public float runRange = 150f;
    public float breakOffRange = 600f;

    public float targetChaseDistance = 30.0f;

    private SpaceshipController myShip;
    private AIShip shipAI;
    public GameObject[] weapons;
    private List<IFireable> weaponScripts;

    private GameObject Whale;
    private GameObject Player;
    private GameObject Enemy;

    private Transform enemyPosition;

    private float decoyTimer;
    private float chaseTimer;
    private float runTimer;
    public float chaseTime;
    public float runTime = 100;

    public bool whaleOnly = false;
    public bool friendly = false;

    public float runAngle = 135f;


    void Start() // set ship to start with Whale as target
    {
        Whale = GameObject.FindGameObjectWithTag("Whale");
        Player = GameObject.FindGameObjectWithTag("Player");
        if (friendly) Enemy = GameObject.FindGameObjectsWithTag("Enemy")[12];
        shipAI = GetComponent<AIShip>();
        myShip = GetComponent<SpaceshipController>();
        weaponScripts = new List<IFireable>();
        //shipAI.TargetPosition = whalePosition.position;
        foreach(GameObject weap in weapons)
        {
            IFireable weapon = weap.GetComponent<IFireable>();
            if (weapon == null) Debug.LogWarning("Weapon was attached with no IFireable");
            else
            {
                weaponScripts.Add(weapon);
                weapon.team = team;
            }
        }
        chaseTimer = 0;
        runTimer = 0;

        if (WhaleStats.instance != null) { decoyTimer = WhaleStats.instance.DecoyLevel * 5; }
        else { decoyTimer = 0; }
    }

    void Update()
    {
        if (friendly)
        {
            DoFriendlyAI();
        }
        else
        {
            DoEnemyAI();
        }
    }

    void DoFriendlyAI ()
    {
        enemyPosition = Enemy.transform;
        float distToTarg = Vector3.Distance(transform.position, enemyPosition.position); //check distance from this Enemy to Player
        Debug.Log(Enemy.gameObject.name);
        CheckFire(enemyPosition);

        if (distToTarg < attackRange)
        {
            curState = AIState.AttackingEnemy;
            Debug.Log("Friendly State now: " + curState);
        }
        else
        {
            curState = AIState.ChasingEnemy;
            ChaseThing(enemyPosition.position, enemyPosition.forward);
        }
        
    }

    void DoEnemyAI ()
    {
        whalePosition = Whale.transform; //find Whale position
        playerPosition = Player.transform;
        float distToPlayer = Vector3.Distance(transform.position, playerPosition.position); //check distance from this Enemy to Player

        CheckFire(playerPosition);
        CheckFire(whalePosition);
        
        switch(curState)
        {
            case AIState.AttackingWhale:
                if (decoyTimer > 0)
                {
                    //Vector3 decoyPosition = Whale.transform.GetChild(1).transform.position;
                    //shipAI.TargetPosition = decoyPosition;
                    //shipAI.ApproachDirection = Vector3.zero;
                    //float angle = Vector3.Angle(transform.position, decoyPosition);
                    //float checkDist = Vector3.Distance(transform.position, decoyPosition);
                    //decoyTimer -= Time.deltaTime;
                }
                if (distToPlayer < attackRange) // pursue player instead of Whale
                {
                    if (!whaleOnly)
                    {
                        curState = AIState.ChasingPlayer;
                    }
                    else
                    {
                        curState = AIState.AttackingWhale;
                        ChaseThing(whalePosition.position, whalePosition.forward);
                    }
                    //Debug.Log("Enemy State now: " + curState);
                }
                else
                {
                    //GetComponent<AIShip>().TargetPosition = whalePosition.position;
                    ChaseThing(whalePosition.position, whalePosition.forward);
                    //Debug.Log("Target: Whale " + GetFollowPosition(whalePosition, 30));
                }
                break;
            case AIState.ChasingPlayer:
                //chaseTimer = chaseTime;
                if (distToPlayer < breakOffRange) // pursue player instead of Whale
                {
                    if (Vector3.Angle(transform.forward, playerPosition.position - transform.position) > runAngle && distToPlayer < runRange)
                    {
                        curState = AIState.RunningFromPlayer;
                        runTimer = runTime;
                        //Debug.Log("Enemy State now: " + curState);
                    }
                    else
                    {
                        ChaseThing(playerPosition.position, playerPosition.forward);
                    }
                }
                else
                {
                    curState = AIState.AttackingWhale;
                    //Debug.Log("Enemy State now: " + curState);
                }
                break;
            case AIState.RunningFromPlayer:
                
                if (runTimer > 0)
                {
                    ChaseThing((playerPosition.position - transform.position) * 100, Vector3.zero);
                    runTimer -= Time.deltaTime;
                }
                else
                {
                    curState = AIState.AttackingWhale;
                    //Debug.Log("Enemy State now: " + curState);
                }
                break;
        }  
        if(whaleOnly) curState = AIState.AttackingWhale;
    }

    private void ChaseThing(Vector3 thing, Vector3 forwardDir)
    {
        shipAI.TargetPosition = thing + (-forwardDir * targetChaseDistance);
        shipAI.ApproachDirection = forwardDir;
    }



    private Vector3 GetFollowPosition(Transform target, float followDistance)
    {
        return target.position + (-target.forward * followDistance);
    }

    private Vector3 getLeadPosition(Transform target)
    {
        SpaceshipController targetShip = target.GetComponent<SpaceshipController>();
        if (targetShip == null) return target.position;

        Vector3 relativeVelocity = myShip.Velocity + targetShip.Velocity;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        float projectileSpeed = weaponScripts.Count > 0 ? (weaponScripts[0].GetProjectileSpeed() + myShip.Velocity.magnitude) : 0;
        float timeToTarget = distanceToTarget / projectileSpeed;
        Vector3 aheadVector = timeToTarget * relativeVelocity;
        return target.position + aheadVector;
    }

    /**
     * Check if a target is close enough, and fires if appropriate
     */
    private void CheckFire(Transform target)
    {
        Vector3 leadPos = getLeadPosition(target);

        float angle = Vector3.Angle(transform.position, leadPos);
        float dist = Vector3.Distance(transform.position, leadPos);

        if (dist < fireDistance && angle < fireAngle)
        {
            foreach (IFireable weap in weaponScripts)
            {
                weap.Fire(this);
            }
        }
    }
}