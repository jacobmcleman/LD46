using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInventory : MonoBehaviour, IInventory
{
    private int chunkSize = 10;

    private int organics = 0;
    private int mechanicals = 0;

    public int MaxOrganics = 100;
    public int MaxMechanicals = 100;

    public int feedDistance = 200;

    [SerializeField]
    public int Organics
    {
        get { return organics; }
        set
        {
            if (value >= MaxOrganics)
            {
                organics = MaxOrganics;
                UIManager.instance.DisplayToolTip("No more room for organic material, return to "+ PlayerPrefs.GetString("whale_name")+" to free up space", 0.5f);
            }
            else { organics = value; }
        }
    }

    [SerializeField]
    public int Mechanicals
    {
        get { return mechanicals; }
        set
        {
            if ( value >= MaxMechanicals)
            {
                mechanicals = MaxMechanicals;
                UIManager.instance.DisplayToolTip("No more room for mechanical material, return to " + PlayerPrefs.GetString("whale_name") + " to free up space", 0.5f);
            }
            else {
                mechanicals = value;
                Debug.Log("Mechanicals: " + mechanicals + " pickedup: " + value);
            }
        }
    } 

    public GameObject OrganicChunk;
    public GameObject MechanicalChunk;

    public GameObject Whale;

    private PlayerHealth h;

    public float regurgitateCooldown = 0.7f;
    private float regurgTimer;

    private Controls controls;

    private void Start()
    {
        h = gameObject.GetComponent<PlayerHealth>();
        Whale = GameObject.FindGameObjectWithTag("Whale");
    }

    void Awake ()
    {
        controls = new Controls();
        controls.PlayerControls.Regurgitate.performed += ctx => Regurgitate();
    }

    private void Update()
    {
        regurgTimer -= Time.deltaTime;

        if (Vector3.Distance(transform.position, Whale.transform.position) < feedDistance)
        {
            //TODO: Notify player they're in range to feeeeeeed
        }
    }

    void Regurgitate()
    {
        if (Vector3.Distance(transform.position, Whale.transform.position) < feedDistance  && regurgTimer <= 0) 
        {
            regurgTimer = regurgitateCooldown;
            if (organics > 0)
            {
                Whale.GetComponent<IInventory>().Organics += organics;
                Organics = 0;
            }
            else if (mechanicals > 0)
            {
                Whale.GetComponent<IInventory>().Mechanicals += mechanicals;
                Mechanicals = 0;
            }
        }
        else if(SceneManager.GetActiveScene().name != "Level1")
        {
            UIManager.instance.DisplayToolTip("Get closer to "+PlayerPrefs.GetString("whale_name")+" to feed "+PlayerPrefs.GetString("whale_name"), 0.5f);
        }
    }

    void SpawnChunk(int amount, GameObject chunkPrefab)
    {
        GameObject chunk = Instantiate(chunkPrefab, (transform.position + transform.forward*5), transform.rotation);
        Pickupables chunkPickup = chunk.GetComponent<Pickupables>();
        chunkPickup.myvalue = amount;
        chunk.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        if (chunk.GetComponent<VomitChunk>() == null)
        {
            Debug.Log("Adding vomitChunk");
            chunk.AddComponent<VomitChunk>();
        }
        chunk.GetComponent<VomitChunk>().StartCoroutine("HuntWhale", Whale);
    }

    void OnEnable ()
    {
        controls.Enable();
    }

    void OnDisable ()
    {
        controls.Disable();
    }
}
