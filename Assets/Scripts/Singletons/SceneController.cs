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
<<<<<<< HEAD
            //Add scene loaded callback function
            SceneManager.sceneLoaded += OnSceneLoaded;
=======
>>>>>>> 67ebef150b0df986a0877f2610d586f1ec3dc728
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

<<<<<<< HEAD
    //Play the intro cinematic and change scene to fly
=======
>>>>>>> 67ebef150b0df986a0877f2610d586f1ec3dc728
    public void LoadIntroCinematic ()
    {
        ChangeScene("Intro");
        StartCoroutine(ChangeSceneAfterDelay("spaceship_test1", 7));
    }

<<<<<<< HEAD
    //Go to the winner screen
    public void WinGame ()
    {
        ChangeScene("Winner");
    }

    //Go to the loser screen
    public void LoseGame ()
    {
        ChangeScene("Loser");
    }

    //Quit game ez
    public void QuitGame ()
    {
        //Quitting is different in editor vs in game, so check to see what to do
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        } 
        else
        {
            Application.Quit();
        }
    }

=======
>>>>>>> 67ebef150b0df986a0877f2610d586f1ec3dc728
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
<<<<<<< HEAD

    //Add callback for when a scene is loaded, maybe want scene specific callback system
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        //If it is loser or winner scene, add the needed listeners to the buttons
        if (scene.name == "Loser" || scene.name == "Winner") {
            GameObject.Find("Menu").GetComponent<Button>().onClick.AddListener(() => instance.ChangeScene("Main Menu"));
            GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(instance.QuitGame);
        }
    }
=======
>>>>>>> 67ebef150b0df986a0877f2610d586f1ec3dc728
}
