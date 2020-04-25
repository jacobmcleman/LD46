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

    public InputActionAsset controls;
    private InputAction fire;

    private bool shooting = false;

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
        
       fire = controls.actionMaps[0].FindAction("PrimaryFire",  true);
       fire.started += StartShootingAction;
       fire.canceled += StopShootingAction;
    }

    void StartShootingAction (InputAction.CallbackContext ctx)
    {
        shooting = true;
    }

    void StopShootingAction (InputAction.CallbackContext ctx)
    {
        shooting = false;
    }

    void OnDisable ()
    {
        fire.started -= StartShootingAction;
        fire.canceled -= StopShootingAction;
    }

    void Update ()
    {
        if(shooting)
        {
            weaponScript1.Fire(this);
            weaponScript2.Fire(this);
        }
    }
}
