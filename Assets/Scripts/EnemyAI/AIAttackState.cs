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

    void Start() // set ship to start with Whale as target
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; 
        GetComponent<AIShip>().TargetPosition = whalePosition.position;
    }

    void Update()
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; //find Whale position
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<AIShip>().TargetPosition = whalePosition.position; //target Whale's postion

        Debug.Log("whalePosition" + whalePosition.position);

        float dist = Vector3.Distance(whalePosition.position,playerPosition.position); //check distance from AI Ship to Player


        if (dist < 200f) // pursue player instead of Whale
        {
            GetComponent<AIShip>().TargetPosition = playerPosition.position;
        }
        else
        {
            GetComponent<AIShip>().TargetPosition = whalePosition.position;

        }
    }
}
