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

    public GameObject resourceDrop;
    
    public EnemyType enemyType;

    private float health;
    
    private bool hasSpawned;
   
    private AudioSource deathSfx;
    private AudioSource hitmarkerSfx;
    private int hitStack = 0;
    private bool hitStackDecayActive = false;

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
            hitStack++;
            float hitmarkerPitch = 1f + (0.2f * hitStack);
            SFXController.instance.PlayHitmarkerSound(hitmarkerSfx, hitmarkerPitch);
            hitStackDecayActive = true;
            return false;
        }
    }

    private IEnumerator DecayHitStack ()
    {
        yield return new WaitForSeconds(1);
        if (hitStack > 1)
        {
            hitStackDecayActive = true;
            hitStack--;
        }
        else
        {
            hitStackDecayActive = false;
            hitStack = 0;
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

    void Update ()
    {
        if (hitStackDecayActive)
        {
            hitStackDecayActive = false;
            StartCoroutine(DecayHitStack());
        }
    }

    void Awake()
    {
        health = MaxHealth;
        hasSpawned = false;
    }

    void Start ()
    {
        foreach (Transform t in GameObject.FindGameObjectWithTag("PlayerSFX").transform)
        {
            if (t.gameObject.tag == "HitmarkerSFX")
            {
                hitmarkerSfx = t.gameObject.GetComponent<AudioSource>();
            }
            if (t.gameObject.tag == "RandSFX")
            {
                deathSfx = t.gameObject.GetComponent<AudioSource>();
            }
            if (deathSfx != null && hitmarkerSfx != null)
            {
                return;
            }
        }
    }

}
