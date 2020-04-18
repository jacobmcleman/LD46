using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int chunkSize = 10;

    private int organics = 0;
    private int mechanicals = 0;

    public int Organics { get { return organics; } }
    public int Mechanicals { get { return mechanicals; } }

    public GameObject OrganicChunk;
    public GameObject MechanicalChunk;

    public GameObject Whale;

    private PlayerHealth h;

    private void Start()
    {
        h = gameObject.GetComponent<PlayerHealth>();
        Whale = GameObject.FindGameObjectWithTag("Whale");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            Regurgitate();
        }
    }

    void Regurgitate()
    {
        while (organics >= 0)
        {
            if (organics <= chunkSize)
            {
                SpawnChunk(chunkSize, OrganicChunk);
                organics -= chunkSize;
            }
            else
            {
                SpawnChunk(organics, OrganicChunk);
                organics = 0;
            }
        }
        while (mechanicals >= 0)
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

    void SpawnChunk(int amount, GameObject chunk)
    {
        Instantiate(chunk, transform.position, transform.rotation);
        Pickupables chunkPickup = chunk.GetComponent<Pickupables>();
        chunkPickup.Initialize(amount);
        chunk.GetComponent<VomitChunk>().StartCoroutine("HuntWhale", Whale);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Pickup")
        {
            Pickupables pickup = col.gameObject.GetComponent<Pickupables>();
            
            switch (pickup.getType())
            {
                case Pickupables.PickupType.Organic:
                    organics += pickup.getValue();
                    break;
                case Pickupables.PickupType.Mechanical:
                    mechanicals += pickup.getValue();
                    break;
                case Pickupables.PickupType.Health:
                    h.Heal(pickup.getValue());
                    break;
            }
        }
    }
}
