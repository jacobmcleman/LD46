using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject Player;
    private GameObject Whale;
    private GameObject Spawner;

    private PlayerShip Ship;
    private IInventory PlayerInventory;
    private IInventory WhaleInventory;
    private IHealth PlayerHealth;
    private IHealth WhaleHealth;
    private Spawner SpawnerCS;

    public DonutAss rocketFill;
    public Text playerHealthText;
    public Text whaleHealthText;
    public Text waveText;
    public Text whaleInorganic;
    public Text whaleOrganic;
    public Text shipInorganic;
    public Text shipOrganic;

    public Slider playerHealthSlider;
    public Slider whaleHealthSlider;

    //Singleton isntance
    public static UIManager instance;

    //*
    //// Unity Methods
    //*

    //On awake, assign the singleton instance
    private void Awake ()
    {
        //Instance should be null, we only want one instance
        if (instance == null)
        {
            //Set the instance to this
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Whale = GameObject.FindGameObjectWithTag("Whale");
        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        PlayerInventory = Player.GetComponent<IInventory>();
        Ship = Player.GetComponent<PlayerShip>();
        WhaleInventory = Whale.GetComponent<IInventory>();
        PlayerHealth = Player.GetComponent<IHealth>();
        WhaleHealth = Whale.GetComponent<IHealth>();
        SpawnerCS = Spawner.GetComponent<Spawner>();

        StartCoroutine(StartRocketCooldown(2f));
    }

    private void Update()
    {
        shipInorganic.text = $"{PlayerInventory.Mechanicals}";
        shipOrganic.text = $"{PlayerInventory.Organics}";
        whaleInorganic.text = $"{WhaleInventory.Mechanicals}";
        whaleOrganic.text = $"{WhaleInventory.Organics}";
        playerHealthText.text = $"{PlayerHealth.Health}/{PlayerHealth.MaxHealth}";
        playerHealthSlider.value = PlayerHealth.Health;
        whaleHealthText.text = $"{WhaleHealth.Health}/{WhaleHealth.MaxHealth}";
        whaleHealthSlider.value = WhaleHealth.Health;
        waveText.text = $"Wave {SpawnerCS.CurWave + 1}/{SpawnerCS.Waves.Count}";
        UpdateFlightHud();
    }

    public void HandleRocketLaunch (float cooldown)
    {
        StartCoroutine(StartRocketCooldown(cooldown));
    }

    private void UpdateFlightHud ()
    {
        Ship.speedSlider.value = Ship.stick.SpeedRatio;
    }


    private IEnumerator StartRocketCooldown (float cooldown)
    {
        rocketFill.Fill = 0.0f;
        while (rocketFill.Fill < 1f)
        {
            yield return null;
            rocketFill.Fill += Time.deltaTime / cooldown;
        }
    }
}
