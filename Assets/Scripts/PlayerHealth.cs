using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float _MaxHealth = 100;

    public float MaxHealth { get { return _MaxHealth; } set { _MaxHealth = value; } }
    public float Health { get { return health; } }

    private float health;

    public void TakeDamage(float damage)
    {
        if (health - damage <= 0)
        {
            //Die
            Destroy(gameObject);
        }
        else
        {
            health -= damage;
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
