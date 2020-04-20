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

    private float attackRange = 300f;

    private AIShip shipAI;
    public GameObject weapon1;
    private IFireable weaponScript1;
    public GameObject weapon2;
    private IFireable weaponScript2;

    void Start() // set ship to start with Whale as target
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; 
        shipAI = GetComponent<AIShip>();
        shipAI.TargetPosition = whalePosition.position;
        weaponScript1 = weapon1.GetComponent<IFireable>();
        weaponScript1.team = team;
        weaponScript2 = weapon2.GetComponent<IFireable>();
        weaponScript2.team = team;
    }

    void Update()
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; //find Whale position
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        float dist = Vector3.Distance(transform.position, playerPosition.position); //check distance from this Enemy to Player
        float angle = Vector3.Angle(transform.position, playerPosition.position);

        if (dist < attackRange) // pursue player instead of Whale
        {
            //GetComponent<AIShip>().TargetPosition = playerPosition.position;
            shipAI.TargetPosition = GetFollowPosition(playerPosition, 30);
            shipAI.ApproachDirection = playerPosition.forward;
            CheckFire(dist, angle);
        }
        else
        {
            //GetComponent<AIShip>().TargetPosition = whalePosition.position;
            shipAI.TargetPosition = GetFollowPosition(whalePosition, 30);
            shipAI.ApproachDirection = whalePosition.forward;
            CheckFire(dist, angle);
        }
    }

    private Vector3 GetFollowPosition(Transform target, float followDistance)
    {
        return target.position + (-target.forward * followDistance);
    }

    /**
     * Check if a target is close enough, and fires if appropriate
     */
    private void CheckFire(float dist, float angle)
    {
        if (dist < fireDistance && angle < fireAngle)
        {
            weaponScript1.Fire(this);
            weaponScript2.Fire(this);
        }
    }
}
