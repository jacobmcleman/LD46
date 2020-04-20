using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour, IFireable
{
    public Transform missileAppearPoint;

    public GameObject Missile;
    public string TargetTag = "Enemy";

    private Transform Target;
    private Targeting targeting;
    public FireableType type { get => FireableType.MissileLauncher; }
    
    private Teams _teams = Teams.enemyTeam;
    public Teams team { 
        get => _teams;
        set { _teams = team; } 
    }
    public float cooldown = 2f;

    private bool canFire = false;

    private void Start()
    {
        targeting = GameObject.FindGameObjectWithTag("Player").GetComponent<Targeting>();
        StartCoroutine(EnableFiring());
    }

    void Update()
    {

    }
    public bool CanFire() 
    {
        return canFire;
    }
    private IEnumerator EnableFiring ()
    {
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }

    private Transform GetBestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetTag);
        float bestTargetVal = float.MinValue;
        Transform bestTarget = null;
        foreach (GameObject enemy in enemies)
        {
            float forwardNess = Vector3.Dot((enemy.transform.position - transform.position).normalized, transform.forward);
            if (forwardNess > bestTargetVal)
            {
                bestTargetVal = forwardNess;
                bestTarget = enemy.transform;
            }
        }

        return bestTarget;
    }


    public bool Fire(IWieldable wielder)
    {
        Debug.Log("boom boom1");
        // if(!canFire)
        // {
        //     Debug.Log("not can fire");
        //     return false;
        // }
        Target = GetBestTarget();
        canFire = false;
        StartCoroutine(EnableFiring());
        LaunchMissile();
        return true;
    }
    private void LaunchMissile()
    {
        Debug.Log(Target);
        UIManager.instance.HandleRocketLaunch(cooldown);
        GameObject missile = Instantiate(Missile, missileAppearPoint.position, missileAppearPoint.rotation);
        MissileController missileController = missile.GetComponent<MissileController>();
        Debug.Log("MIS");
        missileController.Target = Target;
        Rigidbody myRb = GetComponent<Rigidbody>();
        if(myRb != null)
        {
            missile.GetComponent<Rigidbody>().velocity = myRb.velocity;
        }
    }
}
