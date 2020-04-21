using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleInventory : MonoBehaviour, IInventory
{
    private int organics = 0;
    private int mechanicals = 0;

    public int MaxOrganics = 3000;
    public int MaxMechanicals = 3000;

    public int Organics
    {
        get { return organics; }
        set
        {
            if (value >= MaxOrganics)
            {
                organics = MaxOrganics;
                //TODO: tell player max has been hit
            }
            else { organics = value; }
        }
    }
    public int Mechanicals
    {
        get { return mechanicals; }
        set
        {
            if (value >= MaxMechanicals)
            {
                mechanicals = MaxMechanicals;
                //TODO: tell player max has been hit
            }
            else { mechanicals = value; }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
