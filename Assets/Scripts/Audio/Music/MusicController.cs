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

    //Current loop
    public int loop = 0;

    //Blood Sacrifice Clips
    public AudioClip[] bsClips;

    //Death Cult Part 2 Clips
    public AudioClip[] dc2Clips;
    
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
            loop = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //* Public Methods
    //// 
    //*

    //Blood Sacrifice
    public void PlayBloodSacrificeNextSound ()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source.isPlaying && loop < bsClips.Length) {
            if ((loop == 1 || loop == 3) && !staged) {
                Debug.Log(loop);
                StartCoroutine(StageRiser("bloodsack", (source.clip.length - source.time), source, bsClips[loop]));
                loop++;
            } else if (loop == bsClips.Length - 1 && !staged) {
                Debug.Log(loop);
                source.loop = false;
                StartCoroutine(StageLoop(source, bsClips[loop], (source.clip.length - source.time)));
                loop++;
            } else if (!staged) {
                Debug.Log(loop);
                StartCoroutine(StageLoop(source, bsClips[loop], (source.clip.length - source.time)));
                loop++;
            }
        } else if (!staged && loop < bsClips.Length) {
            Debug.Log(loop);
            source.loop = true;
            StartCoroutine(StageLoop(source, bsClips[loop], 0f));
            loop++;
        }
    }

    //Death Cult Part 2
    public void PlayDeathCultPart2NextSound ()
    {
        Debug.Log(staged);
        AudioSource source = GetComponent<AudioSource>();
        if (source.isPlaying  && loop < dc2Clips.Length) {
            if (loop == dc2Clips.Length - 1 && !staged) {
                Debug.Log(loop);
                source.loop = false;
                StartCoroutine(StageLoop(source, dc2Clips[loop], (source.clip.length - source.time)));
                loop++;
            } else if (!staged) {
                Debug.Log(loop);
                source.loop = false;
                StartCoroutine(StageRiser("death cult 2", (source.clip.length - source.time), source, dc2Clips[loop]));
                loop++;
            }
        } else if (!staged && loop < dc2Clips.Length) {
            Debug.Log(loop);  
            source.loop = false;
            StartCoroutine(StageRiser("death cult 2", 0f, source, dc2Clips[loop]));
            loop++;
        }
    }

    //*
    //// Private Methods
    //*

    //This is for staging a loop swap
    private IEnumerator StageLoop (AudioSource source, AudioClip clip, float delay)
    {
        staged = true;
        yield return new WaitForSeconds(delay);
        source.Stop();
        source.clip = clip;
        source.Play();
        staged = false;
    }

    //This is for staging a riser that automatically goes to the next part of the song
    private IEnumerator StageRiser (string curSong, float delay, AudioSource source, AudioClip clip)
    {
        staged = true;
        yield return new WaitForSeconds(delay);
        source.Stop();
        source.clip = clip;
        source.Play();
        staged = false;
        if (curSong == "bloodsack") {
            PlayBloodSacrificeNextSound();
        } else if (curSong == "death cult 2") {
            PlayDeathCultPart2NextSound();
        }
    }

}
