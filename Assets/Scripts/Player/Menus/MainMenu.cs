﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //*
    //// Fields
    //*

    //Whale naming prompt prefab
    [SerializeField]
    private GameObject namingPromptPrefab;

    //Whale naming meme prefab
    [SerializeField]
    private GameObject namingMemePrefab;

    //Normal menu prefab
    [SerializeField]
    private GameObject baseMenuPrefab;

    //*
    //// Unity Methods
    //*

    //Check if the player has named their whale already
    void Start ()
    {
        //Make sure whale isn't name and we haven't been memed yet
        if (PlayerPrefs.HasKey("whale_name") && PlayerPrefs.GetString("whale_name") != "Your Mom"){
            //Whale is already named, trigger meme sequence
            DisplayMenu(namingMemePrefab);
            GameObject.Find("MemePromptHeader").GetComponent<Text>().text = $"You have named your whale {PlayerPrefs.GetString("whale_name")}";
        } else {
            //Whale is not yet named, let them name the whale
            DisplayMenu(namingPromptPrefab);
        }
        GameObject.Find("BaseMenuSub").GetComponent<Text>().text = $"Your whale is named {PlayerPrefs.GetString("whale_name")}";
    }

    //*
    //// Public Methods
    //*
    
    //Set the whales name and go to normal menu
    public void SetWhaleName ()
    {
        //Get name from input field
        string name = GameObject.Find("NameInput").GetComponent<InputField>().text;
        //Make sure name isn't nothing
        if (name == "") {
            //Yell at player
            GameObject.Find("NamePromptHeader").GetComponent<Text>().text = "Cmon bro, type a name";
        } else {
            //Update whale name in palyer prefs
            PlayerPrefs.SetString("whale_name", name);
            //Show base menu
            DisplayMenu(baseMenuPrefab);
        }
        GameObject.Find("BaseMenuSub").GetComponent<Text>().text = $"Your whale is named {PlayerPrefs.GetString("whale_name")}";
    }

    //This player tried to rename their whale, we must meme on them
    public void MemeOnEm ()
    {
        //Set new name in prfs
        PlayerPrefs.SetString("whale_name", "Your mom");
        //Berate the player for being an ass pee
        GameObject.Find("MemePromptHeader").GetComponent<Text>().text = "Wtf bro, why you tryna rename your whale?";
        //Hide the buttons because they are not needed
        GameObject.Find("Rename").SetActive(false);
        GameObject.Find("Keep Name").SetActive(false);
        //Display base menu after a short delay
        StartCoroutine(DisplayMenuAfterDelay(baseMenuPrefab));
    }

    //This player didn't try to rename their whale, let them continue to the menu
    public void ContinueToMenu ()
    {
        //Just change scene ez
        DisplayMenu(baseMenuPrefab);
    }

    //*
    //// Private Methods
    //*

    //Disable all active menu items and enable the specified one
    private void DisplayMenu (GameObject menu)
    {
        //Disable all current menu items
        foreach (Transform t in GameObject.Find("Canvas").transform)
        {
            if (t.gameObject.name != "Whalestrom" && t.gameObject.name != "BaseMenuSub")
            {
                t.gameObject.SetActive(false);
            }
          
        }
        //Enable the new one
        menu.SetActive(true);
        //If its the base menu scene, upate the sub header with the whale name
        if (menu.gameObject.name == "BaseMenu")
        {
            //Also add this method from the scene controller onto our start game button
            //We can't assign this manually in scene because the scene controller gets 
            //Instantiated in the previous scene
            GameObject.Find("Go").GetComponent<Button>().onClick.AddListener(SceneController.instance.LoadIntroCinematic);
            GameObject.Find("Skip").GetComponent<Button>().onClick.AddListener(SceneController.instance.NextLevel);
            GameObject.Find("Skip").GetComponent<Button>().onClick.AddListener(MusicController.instance.PlayNextSong);
            GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(SceneController.instance.QuitGame);
        }
    }

    //Display menu after delay
    private IEnumerator DisplayMenuAfterDelay (GameObject menu)
    {
        yield return new WaitForSeconds(3);
        DisplayMenu(menu);
    }

}