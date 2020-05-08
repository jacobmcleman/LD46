﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MissileLauncher : MonoBehaviour
{
    private Transform Target;
    public Transform leftMissileAppearPoint;
    public Transform rightMissileAppearPoint;

    public GameObject Missile;

    public string TargetTag = "Enemy";

    public float cooldown = 2f;

    private bool canFire = false;

    private bool didLeftLast = false; //I know i know don't @ me

    public float additionalFireSpeed = 10.0f;

    public InputActionAsset controls;
    private InputAction fire;

    private AudioSource missileSfx;

    void Start ()
    {
        StartCoroutine(EnableFiring());
        foreach (Transform t in GameObject.FindGameObjectWithTag("PlayerSFX").transform)
        {
            if (t.gameObject.tag == "MissileSFX")
            {
                missileSfx = t.gameObject.GetComponent<AudioSource>();
                return;
            }
        }
    }

    void Awake ()
    {
        fire = controls.actionMaps[0].FindAction("SecondaryFire", true);
        fire.performed += ShootAction;
    }

    private void OnDisable ()
    {
        fire.performed -= ShootAction;
    }

    void ShootAction (InputAction.CallbackContext ctx)
    {
        Shoot();
    }

    void Shoot()
    {
        if(canFire)
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
        return GetComponent<Targeting>().Target;
    }


    private void LaunchMissile()
    {
        UIManager.instance.HandleRocketLaunch(cooldown);
        SFXController.instance.PlayRocketLaunchCooldownSFX(missileSfx);
        GameObject missile;
        if (didLeftLast)
        {
            missile = Instantiate(Missile, rightMissileAppearPoint.position, rightMissileAppearPoint.rotation);
            didLeftLast = false;
        }
        else
        {
            missile = Instantiate(Missile, leftMissileAppearPoint.position, leftMissileAppearPoint.rotation);
            didLeftLast = true;
        }
        
        MissileController missileController = missile.GetComponent<MissileController>();
        missileController.Target = Target;
        Rigidbody myRb = GetComponent<Rigidbody>();
        if(myRb != null)
        {
            missile.GetComponent<SpaceshipController>().Velocity = myRb.velocity + (transform.forward * additionalFireSpeed);
        }
    }
}
