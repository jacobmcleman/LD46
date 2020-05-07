using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Organic,
    Inorganic
}

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float _MaxHealth = 100;

    public float MaxHealth { get { return _MaxHealth; } set { _MaxHealth = value; } }
    public float Health { get { return health; } }
    public Teams team { get { return Teams.enemyTeam; } }

    private float health;

    public GameObject resourceDrop;

    private bool hasSpawned;

    public EnemyType enemyType;

    private AudioSource deathSfx;
    private AudioSource hitmarkerSfx;

    public bool TakeDamage(float damage, Teams attackerTeam)
    {
        if (team == attackerTeam) 
        {
            Debug.Log("Friendly Hit :: EnemyHealth");

            // Friendly fire
            return false;
        }
        if (health - damage <= 0)
        {
            Debug.Log("Dead Hit :: EnemyHealth");
            SFXController.instance.PlayKillConfirmedSound(hitmarkerSfx);
            SFXController.instance.PlayRandomDeathSound(deathSfx, enemyType);
            //Die
            if (!hasSpawned)
            {
                Instantiate(resourceDrop, transform.position, transform.rotation);
                GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().Enemies.Remove(gameObject); //Remove enemy from enemies list so spawner knows it ded
                GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().Avoids.Remove(gameObject.transform); //Remove enemy from enemies list so spawner knows it ded
                hasSpawned = true;
            }
            Destroy(gameObject);
            return true;
        }
        else
        {
            Debug.Log("Enemy Hit :: EnemyHealth");
            health -= damage;
            SFXController.instance.PlayHitmarkerSound(hitmarkerSfx);
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
        hasSpawned = false;
    }

    void Start ()
    {
        foreach (Transform t in GameObject.FindGameObjectWithTag("Player").transform)
        {
            if (t.gameObject.tag == "RandSFX")
            {
                hitmarkerSfx = t.gameObject.GetComponent<AudioSource>();
            }
        }
        foreach (Transform t in gameObject.transform)
        {
            if (t.gameObject.tag == "RandSFX")
            {
                deathSfx = t.gameObject.GetComponent<AudioSource>();
            }
        }
    }

}
