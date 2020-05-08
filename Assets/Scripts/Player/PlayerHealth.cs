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

    private AudioSource ouchSfx;

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
            SFXController.instance.PlayPlayerDamagedSound(ouchSfx);
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

    void Start()
    {
        foreach (Transform t in GameObject.FindGameObjectWithTag("Player").transform)
        {
            if (t.gameObject.tag == "RandSFX")
            {
                ouchSfx = t.gameObject.GetComponent<AudioSource>();
            }
        }
    }
}
