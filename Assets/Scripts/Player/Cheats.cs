using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cheat"))
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemies[0].GetComponent<IHealth>().TakeDamage(200, Teams.playerTeam);
        }
    }
}
