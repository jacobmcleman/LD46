using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float _MaxHealth = 100;

    public float MaxHealth { get { return _MaxHealth; } set { _MaxHealth = value; } }
    public float Health { get { return health; } }
    public Teams team { get { return Teams.playerTeam; } }

    private float health;

    public bool TakeDamage(float damage, Teams attackerTeam)
    {
        if (team == attackerTeam) 
        {
            // Friendly fire
            return true;
        }
        if (health - damage <= 0)
        {
            //Die
            SceneController.instance.LoseGame();
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
        health = MaxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
