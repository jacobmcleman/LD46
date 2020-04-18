using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupables : MonoBehaviour
{
    public enum PickupType
    {
        Organic,
        Mechanical,
        Health
    }

    public PickupType pickType;
    public int value = 10;

    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }  

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision with: " + col.gameObject.name);
        if (col.transform.parent.tag == "Player")
        {
            switch (pickType)
            {
                case Pickupables.PickupType.Organic:
                    inventory.Organics += value;
                    Debug.Log("Picked up Organics");
                    break;
                case Pickupables.PickupType.Mechanical:
                    inventory.Mechanicals += value;
                    Debug.Log("Picked up Mechanicals");
                    break;
                case Pickupables.PickupType.Health:
                    inventory.gameObject.GetComponent<PlayerHealth>().Heal(value);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
