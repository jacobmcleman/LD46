
/**
 * Class defining basic projectile parameters and methods
 */
public class Projectile : MonoBehavoiur
{
    private void Start()
    {
        // Projectile should only live for timeout seconds
        _hasHit = false;
    }

    private void Update()
    {
        if (hasHit) return;

        transform.forward = direction;
        Vector3 nextPosition = tranform.position + (speed * Time.deltaTime) * direction;

        if (!DoCollisionSweep(nextPosition))
        {
            transform.position = nextPosition;
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
        Vector3 direction = (nextPosition - tranform.postition).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, distance, ~0, Q))
        {
            OnHitSomething(hit.tranform.gameObject, hit.point);
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
        // At point of writing, assuming object has TakeDamage method that 
        // triggers sound and other things aaaa
        bool isDead = other.TakeDamage(this, hitPoint, projectileDamage);
        if (isDead)
        {
            Debug.Log("Target Destroyed");
        }
        _speed = 0;
        _hasHit = true;
        transform.postition = hitPoint;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, persistAfterHitTime);
    }
    public IWeapon shooter;
    public Vector3 direction;
    public float Timeout = 30;
    public float persistAfterHitTime = 1.0f;
    private float _speed;
    public float speed
    {
        get => _speed;
    }
    private float _hasHit = false;
    public float _hasHit
    {
        get => _hasHit;
    }
    public float projectileDamage;
}