using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUps : MonoBehaviour
{
    private int step = 0;
    public bool finished = false;
    public bool coroutineRunning = false;

    public string[] neededInputs;
    public string[] inputTips;

    float durationMultiplier = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begin());
    }

    private IEnumerator Begin () 
    {
        yield return new WaitForSeconds(1);
        UIManager.instance.DisplayToolTip(inputTips[step], 0.00000000004f);
    }

    private IEnumerator IncrementTutorial ()
    {
        while (!finished)
        {
            yield return null;
            if (step == neededInputs.Length)
            {
                finished = true;

            } 
            else if (neededInputs[step] == "Fire1" || neededInputs[step] == "Fire2")
            {
                if (Input.GetButtonDown(neededInputs[step]))
                {
                    step++;
                    UIManager.instance.DisplayToolTip(inputTips[step], 0.00000000004f);
                    Debug.Log("there ya go, nextx one nowww");
                    coroutineRunning = false;
                }
            }
            else if (step < 7)
            {
                if (Input.GetKeyDown(neededInputs[step]))
                {
                    step++;
                    if (step == 7)
                    {   
                        UIManager.instance.DisplayToolTip((inputTips[step] + " " + PlayerPrefs.GetString("whale_name") + " safe!"), 0.000000000004f);
                        FindObjectsOfType<Spawner>()[0].SpawnTutBot();
                    }
                    else
                    {
                        UIManager.instance.DisplayToolTip(inputTips[step], 0.000000000004f);
                    }
                    if (step == 2)
                    {
                        MusicController.instance.PlayBloodSacrificeNextSound();
                    }
                    Debug.Log("there ya go, nextx one nowww");
                    coroutineRunning = false;
                }
            }
            else if (step == 7)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    UIManager.instance.DisplayToolTip("Enemies drop resources.  Fly through them to collect them!", 0.000000000004f);
                    step++;
                    SceneController.instance.ChangeScene("WhaleUpgrade");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!coroutineRunning && !finished)
        {
            StartCoroutine(IncrementTutorial());
            coroutineRunning = true;
        }
        
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
