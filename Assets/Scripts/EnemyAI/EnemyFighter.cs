using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class EnemyFighter : MonoBehaviour
{
    public enum EnemyState {Patrol, Chase, Attack}
    public EnemyState currentState;

    private Vector3 currentPosition;
    public Vector3 distanceToWhale;

    void Start()
    {
        currentPosition = transform.position;
        distanceToWhale = Vector3.zero;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol: // Generic move around
            {
                //TODO: go based on randomized values with SpaceshipController
                break;
            }
            case EnemyState.Chase: // Follow Whale
            {
/*                if (target == null) // resets state if player or whale is out of range
                {
                    currentState = EnemyState.Patrol;
                    return;
                }
                */
                break;
            }
            case EnemyState.Attack: // Attack Fighter
            {
                //TODO: attack using projectile and machine guns
                break;
            }
        }
    }
}
