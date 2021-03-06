﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleHealth : MonoBehaviour, IHealth
{
    public float _MaxHealth = 10000;

    public float MaxHealth { 
        get { return _MaxHealth; } 
        set { _MaxHealth = value; } 
    }
    public float Health { get { return health; } }
    public Teams team { get { return Teams.playerTeam; } }

    private float health;

    public bool invuln = false;

    public bool TakeDamage(float damage, Teams attackerTeam)
    {
        if (WhaleStats.instance != null) { damage = damage * (1 - (.1f * (float)WhaleStats.instance.ArmorLevel)); }
        
        if (team == attackerTeam) {
            // Friendly fire
            Debug.Log("Friendly Hit :: WhaleHealth");
            return false;
        }
        if (health - damage <= 0 && !invuln)
        {
            Debug.Log("Not Friendly && Ded :: WhaleHealth");
            //Die
            Destroy(gameObject);
            return true;
        }
        else if (!invuln)
        {
            Debug.Log("Not Friendly && Not Ded :: WhaleHealth");
            health -= damage;
            return false;
        }
        else
        {
            return false;
        }
    }

    public void Heal(float healAmount)
    {
        if (health + healAmount >= MaxHealth)
        {
            health = MaxHealth;
        }
        else
        {
            health += healAmount;
        }
    }

    void Awake()
    {
        health = MaxHealth;
    }

    void Start()
    {
        if (WhaleStats.instance != null) { health = WhaleStats.instance.HealthPoolLevel * MaxHealth; }
    }

    void Update()
    {
        if (WhaleStats.instance != null) { Heal((WhaleStats.instance.RegenLevel - 1) * Time.deltaTime); }
    }
}
