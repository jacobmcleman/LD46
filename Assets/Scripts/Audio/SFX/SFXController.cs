using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    public AudioClip[] beeps;
    public AudioClip[] cargoLauncherSounds;
    public AudioClip[] combatSounds;
    public AudioClip[] overheatSounds;

    public AudioClip[] gunSounds;
    public AudioClip[] organicExplosions;
    public AudioClip[] inorganicExplosions;

    public AudioClip[] collisions;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayToolTipSFX (AudioSource source)
    {
        source.Stop();
        source.clip = beeps[6];
        source.Play();
    }

    public void PlayResourcePickupSFX (AudioSource source)
    {
        source.Stop();
        source.clip = beeps[7];
        source.Play();

    }

    public void PlayRocketLaunchCooldownSFX (AudioSource source)
    {
        source.Stop();
        source.clip = combatSounds[1];
    }

}
