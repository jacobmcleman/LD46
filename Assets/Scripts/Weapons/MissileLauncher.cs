using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public Transform missileAppearPoint;

    public GameObject Missile;

    private Transform Target;
    private Targeting targeting;

    private void Start()
    {
        targeting = GameObject.FindGameObjectWithTag("Player").GetComponent<Targeting>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Target = targeting.Target;

            LaunchMissile();
        }
    }

    private void LaunchMissile()
    {
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
