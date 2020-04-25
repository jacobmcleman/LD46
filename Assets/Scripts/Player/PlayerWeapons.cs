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
       controls.actionMaps[0].FindAction("PrimaryFire",  true).started += ctx => shooting = true;
       controls.actionMaps[0].FindAction("PrimaryFire",  true).canceled += ctx => shooting = false;
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
