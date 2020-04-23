using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    //*
    //// Fields
    //*

    //Singleton instance
    public static MusicController instance;

    public AudioClip[] songs;
    public int song;

    //Is something staged??
    private bool staged = false;

    //*
    //// Unity Methods
    //*

    //On awake, assign the singleton instance
    private void Awake ()
    {
        //Instance should be null, we only want one instance
        if (instance == null)
        {
            //Set the instance to this
            instance = this;
            //Don't destroy the game object when we load new scenes
            DontDestroyOnLoad(this.gameObject);
            song = 0;
            PlayNextSong();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //* Public Methods
    //// 
    //*

    public void PlayNextSong ()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.Stop();
        if (song == songs.Length)
        {
            song = 0;
        }
        source.clip = songs[song];
        source.Play();
        song++;
        StartCoroutine(StageRiser((source.clip.length - source.time) + 5f));
    }


    //*
    //// Private Methods
    //*

    //This is for staging a riser that automatically goes to the next part of the song
    private IEnumerator StageRiser (float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayNextSong();
    }

}
