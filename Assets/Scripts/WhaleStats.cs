using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleStats : MonoBehaviour
{
    IHealth wHealth;
    public float blood;
    public float armor;
    public float totalWhaleHealth = wHealth.Health.get() * blood * armor;

    public float fireRate; // projectils per second
    public float projectileDmg; // damage of projectile
    
    void Start()
    {

    }

    void Update()
    {
        TakeDamage();
    }


    public void TakeDamage(float amount)
    {
        return totalWhaleHealth - amount;
    }
}