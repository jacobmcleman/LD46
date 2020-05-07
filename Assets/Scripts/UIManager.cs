using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    public Text toolTipText;

    public DonutAss playerHealthSlider;
    public DonutAss whaleHealthSlider;

    public Animator toolTipAnimator;
    private bool isToolTipping;

    public Animator organicPickupAnimator;
    public Text organicPickupText;
    private bool isOrganPickuping;

    public Animator metalPickupAnimator;
    public Text metalPickupText;
    private bool isMetalPickuping;

    public GameObject pauseMenu;
    public GameObject notPauseMenu;

    public Slider overheatSlider;

    private bool pauseToggle = false;

    private AudioSource sfxAudio;

    //Singleton isntance
    public static UIManager instance;

    public InputActionAsset controls;
    private InputAction pause;

    public bool Paused
    {
        get { return pauseToggle; }
    }

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
            pause =  controls.actionMaps[0].FindAction("Pause", true);
            pause.performed += PauseAction;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDisable ()
    {
        pause.performed -= PauseAction;
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Whale = GameObject.FindGameObjectWithTag("Whale");
        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        GameObject.Find("WhaleName").GetComponent<Text>().text = PlayerPrefs.GetString("whale_name");

        PlayerInventory = Player.GetComponent<IInventory>();
        Ship = Player.GetComponent<PlayerShip>();
        WhaleInventory = Whale.GetComponent<IInventory>();
        PlayerHealth = Player.GetComponent<IHealth>();
        WhaleHealth = Whale.GetComponent<IHealth>();
        SpawnerCS = Spawner.GetComponent<Spawner>();
        sfxAudio = GameObject.Find("OtherSFX").GetComponent<AudioSource>();
        StartCoroutine(StartRocketCooldown(2f));
    }

    private void Update()
    {
        shipInorganic.text = $"{PlayerInventory.Mechanicals}";
        shipOrganic.text = $"{PlayerInventory.Organics}";
        whaleInorganic.text = $"{WhaleInventory.Mechanicals}";
        whaleOrganic.text = $"{WhaleInventory.Organics}";
        playerHealthText.text = $"{PlayerHealth.Health}/{PlayerHealth.MaxHealth}";
        playerHealthSlider.Fill = PlayerHealth.Health;
        whaleHealthText.text = $"{WhaleHealth.Health}/{WhaleHealth.MaxHealth}";
        whaleHealthSlider.Fill = WhaleHealth.Health;
        waveText.text = $"Wave {SpawnerCS.CurWave}/{SpawnerCS.Waves.Count}";
        UpdateFlightHud();
    }

    public void PauseAction (InputAction.CallbackContext ctx)
    {
        TogglePause();
    }

    public void TogglePause ()
    {
        if (pauseToggle)
        {
            pauseMenu.SetActive(false);
            notPauseMenu.SetActive(true);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            notPauseMenu.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        pauseToggle = !pauseToggle;
    }

    public void UpdateOverheatUI (float value)
    {
        overheatSlider.value = value;
    }

    public void DisplayToolTip (string tip, float durationMultiplier)
    {
        if (!toolTipAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ready"))
        {
            toolTipAnimator.ResetTrigger("DisplayMessage");
            toolTipAnimator.SetTrigger("Restart");
        } 
        toolTipText.text = tip;
        toolTipAnimator.SetFloat("Duration", durationMultiplier);
        toolTipAnimator.SetTrigger("DisplayMessage");
        if(SFXController.instance) SFXController.instance.PlayToolTipSFX(sfxAudio);
    }

    public void PickupOrganic (string amount)
    {
        if (!organicPickupAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ready"))
        {
            organicPickupAnimator.ResetTrigger("Play");
            organicPickupAnimator.SetTrigger("Restart");
        } 
        organicPickupText.text = "+" + amount;
        organicPickupAnimator.SetTrigger("Play");
        if (SFXController.instance) SFXController.instance.PlayResourcePickupSFX(sfxAudio);
    }

    public void PickupMetal (string amount)
    {
        if (!metalPickupAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ready"))
        {
            metalPickupAnimator.ResetTrigger("Play");
            metalPickupAnimator.SetTrigger("Restart");
        }
        metalPickupText.text = "+" + amount;
        metalPickupAnimator.SetTrigger("Play");
        if(SFXController.instance) SFXController.instance.PlayResourcePickupSFX(sfxAudio);
    }

    public void HandleRocketLaunch (float cooldown)
    {
        StartCoroutine(StartRocketCooldown(cooldown));
    }

    private void UpdateFlightHud ()
    {
        Ship.speedSlider.value = Ship.stick.SpeedRatio;
    }

    public void BtnQuitToMenu ()
    {
        SceneController.instance.ChangeScene("Main Menu");
    }

    public void BtnQuitGame ()
    {
        SceneController.instance.QuitGame();
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
