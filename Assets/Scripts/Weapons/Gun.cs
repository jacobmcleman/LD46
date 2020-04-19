using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IFireable, IWieldable
{
    private Teams _team = Teams.playerTeam;
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
    
    private float cooldownClock = 0f;
    public float cooldownTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        cooldownClock = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownClock += Time.deltaTime;
    }

    public bool CanFire() 
    {
        return cooldownClock >= cooldownTime;
    }

    public bool Fire(IWieldable firer) 
    {
        if(!CanFire())
        {  
            // do stuff
            return false;
        }
        Vector3 fireVec = transform.forward;
        GameObject projectile = GameObject.Instantiate(projectilePrefab);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj == null) Debug.LogError("Tried to shoot not a projectile");
        projectile.transform.position = transform.position + transform.forward;
        proj.team = team;
        proj.direction = fireVec;
        proj.speed += 30;

        cooldownClock = 0;

        return true;
    }
}
