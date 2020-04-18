using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Team team;

    [SerializeField] private LayerMask layerMask;
    public enum EnemyState {Patrol, Chase, Attack}
    private EnemyState currentState;
    private const double V = 3.0;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float stoppingDistance;

    private EnemyAI target;
    public EnemyAI(Vector3 roamPosition) => this.roamPosition = roamPosition; // auto-generated constructor

    private Vector3 velocity;
    private Quaternion desiredRotation;
    private Vector3 direction;

//    private EnemyPathFindingMovement pathFindingMovement;

    private const float attackRange = 5.0f;
    private const float rayDistance = 7.0f;
    private const float disengageDistance = 5.0f;

    private void Awake()
    {
        //pathFindingMovement = GetComponent<EnemyPathFindingMovement>();
    }
    private void Start()
    {
        startingPosition = transform.position;
//        roamPosition = GetRoamingPosition;
    }
    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
            {
                if (NeedsDestination()) {GetDestination();}
                transform.rotation = desiredRotation;
                transform.Translate(translation: Vector3.forward * Time.deltaTime * 5f);
                var rayColor = IsPathBlocked() ? Color.red : Color.green;
//                Debug.DrawRay(start: transform.position, GetRandomDir: direction * rayDistance, rayColor);
                while (IsPathBlocked())
                {
                    GetDestination();
                }
                var targetToChase = CheckForChase();
                if (targetToChase != null)
                {
                    targetToChase = targetToChase.GetComponent<Transform>();
                    currentState = EnemyState.Chase;
                }
                break;
            }
            case EnemyState.Chase:
            {
                if (target == null) {}
                {
                    currentState = EnemyState.Patrol;
                    return;
                }
                
                transform.LookAt(target.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                {
                    currentState = EnemyState.Attack;
                }
                break;
            }
            case EnemyState.Attack:
            {
                if (target != null)
                {
                    Destroy(target.gameObject);
                }
                
                // play laser beam            
                currentState = EnemyState.Patrol;
                break;
            }
        }
    }
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized;
        //randomize vector on X and Y then normalize
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        var hitSomething = Physics.RaycastAll(ray, rayDistance, layerMask);
        return true;
    }
    private void GetDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) +
                               new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f,
                                   UnityEngine.Random.Range(-4.5f, 4.5f));

        roamPosition = new Vector3(testPosition.x, 1f, testPosition.z);

        direction = Vector3.Normalize(roamPosition - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
    }
    private bool NeedsDestination()
    {
        if (roamPosition == Vector3.zero)
            return true;
        var distance = Vector3.Distance(transform.position, roamPosition);
        if (distance <= stoppingDistance)
        {
            return true;
        }
        return false;
    }

    private Vector3 GetPatrolPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f,70f);
    }

    public override bool Equals(object obj)
    {
        return obj is EnemyAI aI &&
               EqualityComparer<Vector3>.Default.Equals(roamPosition, aI.roamPosition);
    }
    // Update is called once per frame

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForChase()
    {
        float chaseRadius = 5f;
        
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for(var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, chaseRadius))
            {
                var drone = hit.collider.GetComponent<EnemyAI>();
                if(drone != null && drone.Team != gameObject.GetComponent<>().Team)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * chaseRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
}
