using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IFireable, IWieldable
{
    private Teams _team = Teams.playerTeam;
    private bool firing;
    private int heat;
    /** effectively the number of consecutive shots the weapon can fire */
    public int maxHeat = 15;
    private float heatClock = 0;
    public float maxCooldownTime = 2f;
    public Teams team 
    {
        get => _team;
        set 
        { 
            _team = team;
        }
    }

    public GameObject projectilePrefab;
    public FireableType type { get { return FireableType.Gun; } }
    public string name { get { return "name"; } }
    
    /** clock that limits the fire rate */
    private float fireClock = 0f;
    /** "time" it takes to fire one shot. */
    public float firePeriod = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fireClock += Time.deltaTime;
        if(firing)
        {
            if(heat > maxHeat)
            {
                heatClock = maxCooldownTime;
                firing = false;
            }
        }
        else
        {
            if (heatClock > 0)
            {
                heatClock -= Time.deltaTime;
            }
            if (heatClock <= 0)
            {
                heatClock = 0;
                heat = 0;
            }
        
        }
    }

    public bool CanFire() 
    {
        Debug.Log(heatClock);
        return (fireClock >= firePeriod) 
            && (heatClock <= 0);
    }

    public bool Fire(IWieldable firer) 
    {
        if(!CanFire())
        {  
            return false;
        }
        firing = true;
        Vector3 fireVec = transform.forward;
        GameObject projectile = GameObject.Instantiate(projectilePrefab);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj == null) Debug.LogError("Tried to shoot not a projectile");
        projectile.transform.position = transform.position + transform.forward;
        proj.team = team;
        float parentSpeed = gameObject.GetComponentInParent<SpaceshipController>()
            .Velocity.magnitude;
        proj.direction = fireVec;
        proj.speed += parentSpeed;

        heat++;
        fireClock = 0;

        return true;
    }
}
