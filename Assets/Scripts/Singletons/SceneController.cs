using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{ 
    //*
    //// Fields
    //*

    //Singleton isntance
    public static SceneController instance { get; private set; }

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
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(InitMenu());
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

    //*
    //// Private Methods
    //*

    //Called on startup after singleton instance is set
    private IEnumerator InitMenu ()
    {
        //Wait for splash screen video to play
        yield return new WaitForSeconds(7);
        ChangeScene("Main Menu");
    }
}
