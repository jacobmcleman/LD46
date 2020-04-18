using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{ 
    //*
    //// Fields
    //*

    //Singleton isntance
    public static SceneController instance;

    //*
    //// Unity Methods
    //*

    //On awake, assign the singleton instance
    private void Awake ()
    {
        //Instance should be null, we only want one instance
        if (instance == null)
        {
            //Set the instance to this and trigger switch to main menu from startup scene
            instance = this;
            //Don't destroy the game object when we load new scenes
            //Make this 7 seconds for real life game
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(ChangeSceneAfterDelay("Main Menu", 1));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //*
    //// Public Methods
    //*

    //Change scene to specified string 
    public void ChangeScene (string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void LoadIntroCinematic ()
    {
        ChangeScene("Intro");
        StartCoroutine(ChangeSceneAfterDelay("spaceship_test1", 7));
    }

    //*
    //// Private Methods
    //*

    //Called on startup after singleton instance is set
    private IEnumerator ChangeSceneAfterDelay (string sceneName, int delay)
    {
        //Change scene after delay
        yield return new WaitForSeconds(delay);
        ChangeScene(sceneName);
    }
}
