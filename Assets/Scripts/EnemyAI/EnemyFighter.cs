using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShip))]
public class EnemyFighter : MonoBehaviour
{
//    public enum EnemyState {Patrol, Chase, Attack}
//    public EnemyState currentState;
    public Transform whalePosition;
    public Transform playerPostion;
   
    bool attackPlayer;

    void Start() // set ship to start with Whale as target
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; 
        GetComponent<AIShip>().TargetPosition = whalePosition.position;
    }

    void Update()
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; //find Whale position
        playerPostion = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<AIShip>().TargetPosition = whalePosition.position; //target Whale's postion

        Debug.Log("whalePosition" + whalePosition.position);

        float dist = Vector3.Distance(whalePosition.position,playerPostion.position); //check distance from AI Ship to Player


        if (dist < 200f) // pursue player instead of Whale
        {
            GetComponent<AIShip>().TargetPosition = playerPostion.position;
            attackPlayer = true;
        }
        else
        {
            attackPlayer = false;
        }

       
    }
}
