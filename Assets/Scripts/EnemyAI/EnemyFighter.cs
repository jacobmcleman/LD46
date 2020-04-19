using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class EnemyFighter : MonoBehaviour
{
    public enum EnemyState {Patrol, Chase, Attack}
    public EnemyState currentState;

    public Transform seekPoint;
    
    void Start()
    {
        GetComponent<AIShip>().TargetPosition = seekPoint.position;
        CurrentPosition = transform.position;
        distanceToWhale = Vector3.zero;
    }

    void Update()
    {
        GetComponent<AIShip>().TargetPosition = seekPoint.position;
        switch (currentState)
        {
            case EnemyState.Patrol: // Generic move around, move at whale at slow but random speed
            {
                    
                    break;
            }
            case EnemyState.Chase: // Follow Whale
            {
                if (InRange()) // resets state if player or whale is out of range
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
            default:
            { break; }
        }
    }
}
