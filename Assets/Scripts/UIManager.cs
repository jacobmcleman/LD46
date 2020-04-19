using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject Player;
    private GameObject Whale;
    private GameObject Spawner;

    private IInventory PlayerInventory;
    private IInventory WhaleInventory;
    private IHealth PlayerHealth;
    private IHealth WhaleHealth;
    private Spawner SpawnerCS;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Whale = GameObject.FindGameObjectWithTag("Whale");
        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        PlayerInventory = Player.GetComponent<IInventory>();
        WhaleInventory = Whale.GetComponent<IInventory>();
        PlayerHealth = Player.GetComponent<IHealth>();
        WhaleHealth = Whale.GetComponent<IHealth>();
        SpawnerCS = Spawner.GetComponent<Spawner>();
    }

    private void Update()
    {
        //PlayerInventory.Mechanicals;
        //PlayerInventory.Organics;
        //WhaleInventory.Mechanicals;
        //WhaleIventory.Organics;
        //PlayerHealth.MaxHealth;
        //PlayerHealth.Health;
        //WhaleHealth.MaxHealth;
        //WhaleHealth.Health;

        //SpawnerCS.CurWave;
        //SpawnerCS.Waves.Length;
    }
}
