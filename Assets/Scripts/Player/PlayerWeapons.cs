using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour, IWieldable
{
    private Teams _team = Teams.playerTeam;
    public Teams team
    {
        get => _team;
        set
        {
            _team = team;
        }
    }

<<<<<<< HEAD
    public InputActionAsset controls;

    private bool shooting = false;
=======
    private Controls controls;

    private float shooting = 0f;
>>>>>>> 9726370544427b0d99a69202f3a358bc65fea15e

    public GameObject weapon1;
    private IFireable weaponScript1;
    public GameObject weapon2;
    private IFireable weaponScript2;

    // Start is called before the first frame update
    void Start()
    {
        weaponScript1 = weapon1.GetComponent<IFireable>();
        weaponScript1.team = team;
        weaponScript2 = weapon2.GetComponent<IFireable>();
        weaponScript2.team = team;
    }

    void Awake ()
    {
<<<<<<< HEAD
       controls.actionMaps[0].FindAction("PrimaryFire",  true).started += ctx => shooting = true;
       controls.actionMaps[0].FindAction("PrimaryFire",  true).canceled += ctx => shooting = false;
=======
        controls = new Controls();
        controls.PlayerControls.PrimaryFire.performed += ctx => shooting = ctx.ReadValue<float>();
>>>>>>> 9726370544427b0d99a69202f3a358bc65fea15e
    }

    void Update ()
    {
<<<<<<< HEAD
        if(shooting)
=======
        if(shooting == 1f)
>>>>>>> 9726370544427b0d99a69202f3a358bc65fea15e
        {
            weaponScript1.Fire(this);
            weaponScript2.Fire(this);
        }
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
