using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShip))]
public class AIAttackState : MonoBehaviour
{
//    public enum EnemyState {Patrol, Chase, Attack}
//    public EnemyState currentState;
    public Transform whalePosition;
    public Transform playerPosition;

    private float attackRange = 300f;

    private AIShip shipAI;

    void Start() // set ship to start with Whale as target
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; 
        shipAI = GetComponent<AIShip>();
        shipAI.TargetPosition = whalePosition.position;
    }

    void Update()
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; //find Whale position
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        float dist = Vector3.Distance(transform.position,playerPosition.position); //check distance from this Enemy to Player

        if (dist < attackRange) // pursue player instead of Whale
        {
            //GetComponent<AIShip>().TargetPosition = playerPosition.position;
            shipAI.TargetPosition = GetFollowPosition(playerPosition, 30);
            shipAI.ApproachDirection = playerPosition.forward;
        }
        else
        {
            //GetComponent<AIShip>().TargetPosition = whalePosition.position;
            shipAI.TargetPosition = GetFollowPosition(whalePosition, 30);
            shipAI.ApproachDirection = whalePosition.forward;
        }
    }

    private Vector3 GetFollowPosition(Transform target, float followDistance)
    {
        return target.position + (-target.forward * followDistance);
    }
}
