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
    string leftClick = "Left click to use machine gun";
    string rightClick= "Right click to use missles";
    string leftShift= "Left Shift to boost";

    float durationMultiplier = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

/*
        while (!Input.GetKeyDown(KeyCode.W))
        {
            UIManager.instance.DisplayToolTip(string throttleUp, float durationMultiplier);
        }
        while (!Input.GetKeyDown(KeyCode.S))
        {
            UIManager.instance.DisplayToolTip(string throttleDown, float durationMultiplier);
        }
        while (!Input.GetKeyDown(KeyCode.A))
        {
            UIManager.instance.DisplayToolTip(string rollLeft, float durationMultiplier);
        }
        while (!Input.GetKeyDown(KeyCode.D))
        {
            UIManager.instance.DisplayToolTip(string rollRight, float durationMultiplier);
        }
        while (!Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.instance.DisplayToolTip(string yawLeft, float durationMultiplier);
        }
        while (!Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.DisplayToolTip(string yawRight, float durationMultiplier);
        }
        while (!Input.GetButtonDown("Fire1"))
        {
            UIManager.instance.DisplayToolTip(string leftClick, float durationMultiplier);
        }
        while (!Input.GetButtonDown("Fire2"))
        {
            UIManager.instance.DisplayToolTip(string rightClick, float durationMultiplier);
        }
        while (Input.GetKey(KeyCode.LeftShift))
        {
            UIManager.instance.DisplayToolTip(string leftShift, float durationMultiplier);
        }
*/


/* commented out for debuging

        while (Input.GetKeyDown(KeyCode.t"))
        {
            Debug.Log("T was pushed");
        }
*/
    }
}
