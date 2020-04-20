using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleHealth : MonoBehaviour, IHealth
{
    public float _MaxHealth = 100;

    public float MaxHealth { get { return _MaxHealth; } set { _MaxHealth = value; } }
    public float Health { get { return health; } }
    public Teams team { get { return Teams.playerTeam; } }

    private float health;

    public bool TakeDamage(float damage, Teams attackerTeam)
    {
        damage = damage * (1 - (.1f * (float)WhaleStats.instance.ArmorLevel));
        Debug.Log("HIT :: WhaleHealth");
        if (team == attackerTeam) {
            // Friendly fire
            Debug.Log("Friendly Hit :: WhaleHealth");
            return true;
        }
        if (health - damage <= 0)
        {
            //Die
            Destroy(gameObject);
            return false;
        }
        else
        {
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
        MaxHealth = WhaleStats.instance.HealthPoolLevel * MaxHealth;
        health = MaxHealth;
    }

    void Update()
    {
        Heal(WhaleStats.instance.RegenLevel * Time.deltaTime);
    }
}
