using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoralPopUps : MonoBehaviour
{
    string throttleUp = "Press W to go faster";
    string throttleDown = "Press S to go slower";
    string rollLeft = "Press A to roll left";
    string rollRight= "Press D to roll right";
    string yawLeft = "Press W to yaw left";
    string yawRight = "Press S to yaw right";
//    string bankLeft = "Press A to bank left";
//    string bankRight= "Press D to bank right";    

    float durationMultiplier = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*
        while (!Input.GetKeyDown("w"))
        {
            UIManager.instance.DisplayToolTip(string throttleUp, float durationMultiplier);
        }
        while (!Input.GetKeyDown("s"))
        {
            UIManager.instance.DisplayToolTip(string throttleDown, float durationMultiplier);
        }
        while (!Input.GetKeyDown("a"))
        {
            UIManager.instance.DisplayToolTip(string rollLeft, float durationMultiplier);
        }
        while (!Input.GetKeyDown("d"))
        {
            UIManager.instance.DisplayToolTip(string rollRight, float durationMultiplier);
        }
        while (!Input.GetKeyDown("q"))
        {
            UIManager.instance.DisplayToolTip(string yawLeft, float durationMultiplier);
        }
        while (!Input.GetKeyDown("e"))
        {
            UIManager.instance.DisplayToolTip(string yawRight, float durationMultiplier);
        }
*/
        while (Input.GetKeyDown("t"))
        {
            Debug.Log("T was pushed");
        }       
    }

}
