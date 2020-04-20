using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class defining basic projectile parameters and methods
 */
public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float Timeout = 30;
    public float persistAfterHitTime = 1.0f;
    public float speed = 150;
    private bool _hasHit = false;
    public bool hasHit
    {
        get => _hasHit;
    }
    public float projectileDamage;
    private Teams _team = Teams.playerTeam;
    public Teams team
    {
        get => _team;
        set
        {
            _team = team;
        }
    }
    private void Start()
    {
        // Projectile should only live for timeout seconds
        _hasHit = false;
    }

    private void Update()
    {
        if (hasHit) return;

        this.transform.forward = direction;
        Vector3 nextPosition = this.transform.position + (speed * Time.deltaTime) * direction;

        if (!DoCollisionSweep(nextPosition))
        {
            this.transform.position = nextPosition;
        }

    }

    /**
     * This does the actual collision sweeping
     * @param nextPosition - the next position of the object. This is only used
     *  to calculate the direction of the sweep
     * @return true if hit; false otherwise
     */
    private bool DoCollisionSweep(Vector3 nextPosition)
    {
        float distance = (speed * Time.deltaTime) * 1.5f;
        Vector3 direction = (nextPosition - this.transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, 
            out hit, distance, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore))
        {
            OnHitSomething(hit.transform.gameObject, hit.point);
            return true;
        }
        return false;
    }
    /** 
     * @param other - the game object that was hit
     * @param hitPoint - the point at which the hit happened (different from the
     *  object's tranform)
     */
    private void OnHitSomething(GameObject other, Vector3 hitPoint)
    {
        Debug.Log("HIT");
        // At point of writing, assuming object has TakeDamage method that 
        // triggers sound and other things.
        bool isDead = false;
        IHealth health = other.GetComponent<IHealth>();
        if(health != null)
        {
            Debug.Log("takin damag");
            isDead = health.TakeDamage(projectileDamage, team);
        }
        if (isDead)
        {
            Debug.Log("Target Destroyed");
        }
        speed = 0;
        _hasHit = true;
        this.transform.position = hitPoint;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, persistAfterHitTime);
    }
}