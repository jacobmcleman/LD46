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
    public int myvalue = 10;

    public UIManager ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //This is super fucking jank, I'm sorry
        Debug.Log("Collision with: " + collider.gameObject.name);
        if (collider.tag == "PlayerCol" || collider.tag == "WhaleCol")
        {
            GameObject col;
            if (collider.tag == "PlayerCol") { col = GameObject.FindGameObjectWithTag("Player"); }
            else { col = GameObject.FindGameObjectWithTag("Whale"); }
            switch (pickType)
            {
                case Pickupables.PickupType.Organic:
                    col.GetComponent<IInventory>().Organics += myvalue;
                    if (col.tag == "Player") { ui.PickupOrganic(myvalue.ToString()); }
                    Debug.Log("Picked up Organics");
                    break;
                case Pickupables.PickupType.Mechanical:
                    col.GetComponent<IInventory>().Mechanicals += myvalue;
                    if (col.tag == "Player") { ui.PickupMetal(myvalue.ToString()); }
                    Debug.Log("Picked up Mechanicals");
                    break;
                case Pickupables.PickupType.Health:
                    col.GetComponent<IHealth>().Heal(myvalue);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
