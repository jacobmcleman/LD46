using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShip))]
public class MissileController : MonoBehaviour
{
    public Teams team;

    public float damage = 20;

    public float breakoffAngle = 30f;

    public float trackingGiveUpTime = 15f;
    public float noTargetGiveUp = 3;

    public GameObject explosionPrefab;

    private Transform target;
    private AIShip shipAI;

    private Vector3 lastKnownDirection;
    private float stateTimer;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    private void Start()
    {
        shipAI = GetComponent<AIShip>();
        lastKnownDirection = transform.forward;
        stateTimer = 0.0f;
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;

        if (target != null)
        {
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            lastKnownDirection = dirToTarget;

            if(stateTimer > trackingGiveUpTime)
            {
                Explode();
            }

            if (Vector3.Angle(dirToTarget, transform.forward) > breakoffAngle)
            {
                Debug.Log("Target lost!");
                target = null;
                stateTimer = 0.0f;
            }
            else
            {
                shipAI.TargetPosition = target.position;
            }
        }
        else
        {
            // Continue is last known direction
            shipAI.TargetPosition = transform.position + (lastKnownDirection * 100);

            if (stateTimer > noTargetGiveUp)
            {
                Explode();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();

        IHealth hit = collision.gameObject.GetComponent<IHealth>();
        if (hit != null)
        {
            hit.TakeDamage(damage, team);
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 15f);
        Destroy(gameObject);
        if(SFXController.instance) SFXController.instance.PlayRocketExplosionSFX(explosion.GetComponent<AudioSource>());
    }
}
