using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShip))]
public class JellyFish : MonoBehaviour
{
    public Transform whalePosition;

    private AIShip jellyAI;

    void Start()
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; 
        jellyAI = GetComponent<AIShip>();
        jellyAI.TargetPosition = whalePosition.position;
    }

    void Update()
    {
        whalePosition = GameObject.FindGameObjectWithTag("Whale").transform; //find Whale position
        jellyAI.TargetPosition = whalePosition.position;
        Debug.Log("Jellyfish Position" + transform.position);
    }
}
