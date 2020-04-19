using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IFireable, IWieldable
{
    public GameObject projectilePrefab;
    public FireableType type { get { return FireableType.Gun; } }
    public string name { get { return "name"; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire(this);
        }
        
    }

    public bool CanFire() 
    {
        return true;
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
        proj.direction = fireVec;
        proj.speed += 30;

        return true;
    }
}
