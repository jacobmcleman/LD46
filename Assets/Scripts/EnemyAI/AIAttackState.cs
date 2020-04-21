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
            _team = team;
        }

    }

    private enum AIState
    {
        AttackingWhale,
        ChasingPlayer,
        RunningFromPlayer
    }
    private AIState curState;

    public float attackRange = 200f;
    public float runRange = 150f;
    public float breakOffRange = 600f;

    public float targetChaseDistance = 30.0f;

    private SpaceshipController myShip;
    private AIShip shipAI;
    public GameObject weapon1;
    private IFireable weaponScript1;
    public GameObject weapon2;
    private IFireable weaponScript2;

    private GameObject Whale;
    private GameObject Player;

    public GameObject notPlayerPrefab;

    private float decoyTimer;
    private float chaseTimer;
    private float runTimer;
    public float chaseTime;
    public float runTime = 100;

    public float runAngle = 135f;


    void Start() // set ship to start with Whale as target
    {
        Whale = GameObject.FindGameObjectWithTag("WhaleMouth");
        Player = GameObject.FindGameObjectWithTag("Player");
        shipAI = GetComponent<AIShip>();
        myShip = GetComponent<SpaceshipController>();
        //shipAI.TargetPosition = whalePosition.position;
        weaponScript1 = weapon1.GetComponent<IFireable>();
        weaponScript1.team = Teams.enemyTeam;
        weaponScript2 = weapon2.GetComponent<IFireable>();
        weaponScript2.team = Teams.enemyTeam;

        chaseTimer = 0;
        runTimer = 0;

        if (WhaleStats.instance != null) { decoyTimer = WhaleStats.instance.DecoyLevel * 5; }
        else { decoyTimer = 0; }
    }

    void Update()
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
                    curState = AIState.ChasingPlayer;
                    Debug.Log("Enemy State now: " + curState);
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
                        Debug.Log("Enemy State now: " + curState);
                    }
                    else
                    {
                        ChaseThing(playerPosition.position, playerPosition.forward);
                    }
                }
                else
                {
                    curState = AIState.AttackingWhale;
                    Debug.Log("Enemy State now: " + curState);
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
                    Debug.Log("Enemy State now: " + curState);
                }
                break;
        }  
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
        float projectileSpeed = weaponScript1.GetProjectileSpeed() + myShip.Velocity.magnitude;
        float timeToTarget = distanceToTarget / weaponScript1.GetProjectileSpeed();
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
            if (weaponScript1 != null)
            {
                weaponScript1.Fire(this);
            }
            if (weaponScript2 != null)
            {
                weaponScript2.Fire(this);
            }
        }
    }
}
