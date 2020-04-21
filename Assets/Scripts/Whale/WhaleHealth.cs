using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleHealth : MonoBehaviour, IHealth
{
    public float _MaxHealth = 10000;

    public float MaxHealth { 
        get { return _MaxHealth; } 
        set { _MaxHealth = MaxHealth; } 
    }
    public float Health { get { return health; } }
    public Teams team { get { return Teams.playerTeam; } }

    private float health;

    public bool TakeDamage(float damage, Teams attackerTeam)
    {
        if (WhaleStats.instance != null) { damage = damage * (1 - (.1f * (float)WhaleStats.instance.ArmorLevel)); }
        
        if (team == attackerTeam) {
            // Friendly fire
            Debug.Log("Friendly Hit :: WhaleHealth");
            return true;
        }
        if (health - damage <= 0)
        {
            Debug.Log("Not Friendly && Ded :: WhaleHealth");
            //Die
            Destroy(gameObject);
            return false;
        }
        else
        {
            Debug.Log("Not Friendly && Not Ded :: WhaleHealth");
            health -= damage;
            return true;
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
        if (WhaleStats.instance != null) { health = WhaleStats.instance.HealthPoolLevel * MaxHealth; }
        health = MaxHealth;
    }

    void Update()
    {
        if (WhaleStats.instance != null) { Heal(WhaleStats.instance.RegenLevel * Time.deltaTime); }
    }
}
