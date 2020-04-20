using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    private Transform Target;
    public Transform missileAppearPoint;

    public GameObject Missile;

    public string TargetTag = "Enemy";

    public float cooldown = 2f;

    private bool canFire = false;

    void Start ()
    {
        StartCoroutine(EnableFiring());
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2") && canFire)
        {
            Target = GetBestTarget();
            LaunchMissile();
            canFire = false;
            StartCoroutine(EnableFiring());
        }
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

    private void LaunchMissile()
    {
        UIManager.instance.HandleRocketLaunch(cooldown);
        GameObject missile = Instantiate(Missile, missileAppearPoint.position, missileAppearPoint.rotation);
        MissileController missileController = missile.GetComponent<MissileController>();
        missileController.Target = Target;
        Rigidbody myRb = GetComponent<Rigidbody>();
        if(myRb != null)
        {
            missile.GetComponent<Rigidbody>().velocity = myRb.velocity;
        }
    }
}
