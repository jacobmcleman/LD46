using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPopUps : MonoBehaviour
{
    private int step;
    public bool finished = false;
    public bool coroutineRunning = false;

    public string[] inputTips;
    public string[] expectedInputActions;

    private Spawner sp;

    private PlayerInput input;

    //*
    //// This is a fucking train wreck
    //*

    void Start()
    {
        sp = FindObjectOfType<Spawner>();
        sp.SpawnTutBot();
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        StartCoroutine(Begin());
        step = 0;
    }

    private IEnumerator Begin () 
    {
        yield return new WaitForSeconds(1);
        DisplayPrompt(input.actions[expectedInputActions[step]].GetBindingDisplayString());

    }

    private IEnumerator IncrementTutorial ()
    {
        //Good fucking luck figuring this out I wrote it and it still takes me a few seconds to explain what any given thing does
        //How else would you do this??????? I need to wait?  And check?  and keep checking?  maybe instead of the stupid boolean
        //triggers call on update thing I could just call the coroutine directly wherever I set coroutineRunning = false?
        //I don't care enough to try right now I already put way too much time into this today and still need to do the fuckin
        //rebind menu
        while (!finished)
        {
            yield return null;
            if (step == 12)
            {
                finished = true;

            } 
            else if (step <  8)
            {
                if (input.actions[expectedInputActions[step]].triggered)
                {
                    step++;
                    if (step == 8) 
                    {
                        UIManager.instance.DisplayToolTip("Keep " + PlayerPrefs.GetString("whale_name") + " safe! Red indicators point to the nearest enemy. Destroy the enemy to continue.", 0.000000000004f);
                    }
                    else 
                    {
                        DisplayPrompt(input.actions[expectedInputActions[step]].GetBindingDisplayString());
                    }
                    coroutineRunning = false;
                }
            }
            else
            {
                if (step == 8)
                {
                    if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && sp.startedSpawningEnemy == true)
                    {
                        UIManager.instance.DisplayToolTip("Enemies drop resources.  Fly through them to collect them!  Blue indicators point to resource pickups.", 0.000000000004f);
                        step++;
                        coroutineRunning = false;
                    }
                }
                else if (step == 9)
                {
                    if (GameObject.FindGameObjectsWithTag("Resource").Length == 0)
                    {
                        UIManager.instance.DisplayToolTip("Finally, fly up next to " + 
                            PlayerPrefs.GetString("whale_name") + 
                            " and press (" + input.actions["Regurgitate"].GetBindingDisplayString() + ") to deposit your resources!", 
                            0.000000000004f
                        );
                        step++;
                        coroutineRunning = false;
                    }
                }
                else if (step == 10)
                {
                    if (GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventory>().Mechanicals == 0 && GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventory>().Organics == 0)
                    {
                        UIManager.instance.DisplayToolTip("You have completed the tutorial!  Press (" + 
                            input.actions["Continue"].GetBindingDisplayString() + 
                            ") to continue to the upgrade screen.",
                             0.000000000004f
                        );
                        step++;
                        coroutineRunning = false;
                    }
                }
                else if (step == 11)
                {
                    if (input.actions["Continue"].triggered)
                    {
                        step++;
                        coroutineRunning = false;
                        sp.SetWhaleStats();
                        SceneController.instance.WinLevel();
                    }
                }
            }
        }
    }

    private void DisplayPrompt (string str)
    {
        UIManager.instance.DisplayToolTip
        (
            inputTips[step].Replace("%", "(" + str + ")"
            ), 0.00000000004f
        );
    }

    void Update ()
    {
        if (!coroutineRunning && !finished)
        {
            StartCoroutine(IncrementTutorial());
            coroutineRunning = true;
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
