using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Controls controls;

    private float shooting = 0f;

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
        controls = new Controls();
        controls.PlayerControls.PrimaryFire.performed += ctx => shooting = ctx.ReadValue<float>();
    }

    void Update ()
    {
        if(shooting == 1f)
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
