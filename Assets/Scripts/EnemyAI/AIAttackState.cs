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

    private GameObject Whale;
    private GameObject Player;
    private float decoyTimer;

    void Start() // set ship to start with Whale as target
    {
        Whale = GameObject.FindGameObjectWithTag("Whale");
        Player = GameObject.FindGameObjectWithTag("Player");
        shipAI = GetComponent<AIShip>();
        //shipAI.TargetPosition = whalePosition.position;
        weaponScript1 = weapon1.GetComponent<IFireable>();
        weaponScript1.team = team;
        weaponScript2 = weapon2.GetComponent<IFireable>();
        weaponScript2.team = team;

        if (WhaleStats.instance != null) { decoyTimer = WhaleStats.instance.DecoyLevel * 5; }
        else { decoyTimer = 0; }
    }

    void Update()
    {
        whalePosition = Whale.transform; //find Whale position
        playerPosition = Player.transform;

        float dist = Vector3.Distance(transform.position, playerPosition.position); //check distance from this Enemy to Player
        

        if (decoyTimer > 0)
        {
            Vector3 decoyPosition = Whale.transform.GetChild(1).transform.position;
            shipAI.TargetPosition = decoyPosition;
            shipAI.ApproachDirection = Vector3.zero;
            float angle = Vector3.Angle(transform.position, decoyPosition);
            float checkDist = Vector3.Distance(transform.position, decoyPosition);
            CheckFire(checkDist, angle);
            decoyTimer -= Time.deltaTime;
        }
        if (dist < attackRange) // pursue player instead of Whale
        {
            //GetComponent<AIShip>().TargetPosition = playerPosition.position;
            shipAI.TargetPosition = GetFollowPosition(playerPosition, 30);
            shipAI.ApproachDirection = playerPosition.forward;
            float angle = Vector3.Angle(transform.position, playerPosition.position);
            float checkDist = Vector3.Distance(transform.position, playerPosition.position);
            CheckFire(checkDist, angle);
        }
        else
        {
            //GetComponent<AIShip>().TargetPosition = whalePosition.position;
            shipAI.TargetPosition = GetFollowPosition(whalePosition, 30);
            shipAI.ApproachDirection = whalePosition.forward;
            float angle = Vector3.Angle(transform.position, whalePosition.position);
            float checkDist = Vector3.Distance(transform.position, whalePosition.position);
            CheckFire(checkDist, angle);
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
