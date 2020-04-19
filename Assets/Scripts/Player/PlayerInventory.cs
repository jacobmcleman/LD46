using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{
    private int chunkSize = 10;

    private int organics = 0;
    private int mechanicals = 0;

    public int MaxOrganics = 100;
    public int MaxMechanicals = 100;

    public int Organics
    {
        get { return organics; }
        set
        {
            if (value >= MaxOrganics)
            {
                organics = MaxOrganics;
                //TODO: tell player max has been hit
            }
            else { organics = value; }
        }
    }
    public int Mechanicals
    {
        get { return mechanicals; }
        set
        {
            if ( value >= MaxMechanicals)
            {
                mechanicals = MaxMechanicals;
                //TODO: tell player max has been hit
            }
            else { mechanicals = value; }
        }
    } 

    public GameObject OrganicChunk;
    public GameObject MechanicalChunk;

    public GameObject Whale;

    private PlayerHealth h;

    public float regurgitateCooldown = 0.7f;
    private float regurgTimer;

    private void Start()
    {
        h = gameObject.GetComponent<PlayerHealth>();
        Whale = GameObject.FindGameObjectWithTag("Whale");
    }

    private void Update()
    {
        regurgTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && regurgTimer <= 0) 
        {
            Regurgitate();
            regurgTimer = regurgitateCooldown;
        }
    }

    void Regurgitate()
    {
        if (organics > 0)
        {
            if (organics >= chunkSize)
            {
                SpawnChunk(chunkSize, OrganicChunk);
                organics -= chunkSize;
                Debug.Log("Organics -= 10. organics:" + organics);
            }
            else
            {
                SpawnChunk(organics, OrganicChunk);
                organics = 0;
            }
        }
        else if (mechanicals > 0)
        {
            if (mechanicals >= chunkSize)
            {
                SpawnChunk(chunkSize, MechanicalChunk);
                mechanicals -= chunkSize;
            }
            else
            {
                SpawnChunk(mechanicals, MechanicalChunk);
                mechanicals = 0;
            }
        }
    }

    void SpawnChunk(int amount, GameObject chunkPrefab)
    {
        GameObject chunk = Instantiate(chunkPrefab, (transform.position + transform.forward*5), transform.rotation);
        Pickupables chunkPickup = chunk.GetComponent<Pickupables>();
        chunkPickup.value = amount;
        chunk.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        if (chunk.GetComponent<VomitChunk>() == null)
        {
            Debug.Log("Adding vomitChunk");
            chunk.AddComponent<VomitChunk>();
        }
        chunk.GetComponent<VomitChunk>().StartCoroutine("HuntWhale", Whale);
    }
}
