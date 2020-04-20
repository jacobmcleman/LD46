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

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision with: " + collider.gameObject.name);
        if (collider.transform.tag == "Player" || collider.transform.tag == "Whale")
        {
            GameObject col;
            if (collider.transform.parent.tag == "Player") { col = collider.transform.parent.gameObject; }
            else { col = collider.gameObject; }
            switch (pickType)
            {
                case Pickupables.PickupType.Organic:
                    col.GetComponent<IInventory>().Organics += value;
                    Debug.Log("Picked up Organics");
                    break;
                case Pickupables.PickupType.Mechanical:
                    col.GetComponent<IInventory>().Mechanicals += value;
                    Debug.Log("Picked up Mechanicals");
                    break;
                case Pickupables.PickupType.Health:
                    col.GetComponent<IHealth>().Heal(value);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
