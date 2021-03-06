﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IFireable, IWieldable
{
    public Teams realteam = Teams.playerTeam;
    private bool firing;
    private float heat;
    /** effectively the number of consecutive shots the weapon can fire */
    public float maxHeat = 15;
    public float heatDecayRate = 0.5f;
    public float shotHeatCost = 0.6f;
    private float heatClock = 0;
    public float maxCooldownTime = 1f;
    public float spread = .01f;
    public Teams team 
    {
        get => realteam;
        set 
        { 
            realteam = value;
        }
    }

    public GameObject projectilePrefab;
    public FireableType type { get { return FireableType.Gun; } }
    public string name { get { return "name"; } }
    
    /** clock that limits the fire rate */
    private float fireClock = 0f;
    /** "time" it takes to fire one shot. */
    public float firePeriod = 0.25f;

    private AudioSource sfxAudio;
    private bool warned = false;

    public float overheatBlinkRate = 0.2f;
    public bool overheatEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        sfxAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        fireClock += Time.deltaTime;

        if(heat > 0)
        {
            heat -= Time.deltaTime * heatDecayRate;
        }
        
        if(firing && overheatEnabled)
        {
            if(heat > maxHeat)
            {
                heatClock = maxCooldownTime;
                firing = false;
                if(SFXController.instance) SFXController.instance.PlayGunOverheat(sfxAudio);
            }
            else if (heat > maxHeat - 4 && !warned)
            {
                warned = true;
                if (SFXController.instance) SFXController.instance.PlayOverheatWarning(sfxAudio, transform.position);
                UIManager.instance.UpdateOverheatUI(heat / maxHeat);
            }
            else
            {
                UIManager.instance.UpdateOverheatUI(heat / maxHeat);
            }
        }
        else if (realteam == Teams.playerTeam)
        {
            if (heatClock > 0)
            {
                heatClock -= Time.deltaTime;
                UIManager.instance.UpdateOverheatUI(((int)(heatClock / overheatBlinkRate) % 2));
            }
            if (heatClock <= 0)
            {
                heatClock = 0;
                heat = 0;
                warned = false;
                UIManager.instance.UpdateOverheatUI(0);
            }
        }
    }

    public float GetProjectileSpeed()
    {
        Projectile proj = projectilePrefab.GetComponent<Projectile>();
        if (proj == null) return 0.0f;
        else return proj.speed;
    }

    public bool CanFire() 
    {
        //Debug.Log(heatClock);
        return (fireClock >= firePeriod) 
            && (heatClock <= 0);
    }

    public bool Fire(IWieldable firer) 
    {
        if(!CanFire())
        {  
            return false;
        }

        //Debug.Log("Shooting projectile for team " + team);

        firing = true;
        Vector3 fireVec = transform.forward;
        GameObject projectile = Instantiate(projectilePrefab);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj == null) Debug.LogError("Tried to shoot not a projectile");
        projectile.transform.position = transform.position + transform.forward;
        proj.team = team;
        float parentSpeed = gameObject.GetComponentInParent<SpaceshipController>()
            .Velocity.magnitude;
        proj.direction = fireVec;
        proj.direction.x += Random.Range(-spread, spread);
        proj.direction.y += Random.Range(-spread, spread);
        proj.direction.z += Random.Range(-spread, spread);
        proj.speed += parentSpeed;
        heat += shotHeatCost;
        fireClock = 0;
        float heatMod = heat;
        float maxHeatMod = maxHeat;
        float pitchBend = Random.Range(.1f, 1f);
        if (overheatEnabled)
        {
            pitchBend = heatMod / maxHeatMod + 0.3f;
        }     
        if(SFXController.instance != null) SFXController.instance.PlayRNGGunShot(sfxAudio, pitchBend, transform.position);
        return true;
    }
}
