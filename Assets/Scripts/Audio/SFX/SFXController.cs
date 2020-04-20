﻿using System.Collections;
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

    public UnityEngine.Audio.AudioMixer mixer;

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
        StartCoroutine(PlayAfterDelay(source, 1f));
    }

    public void PlayRocketExplosionSFX (AudioSource source)
    {
        source.Stop();
        int rand = Random.Range(0, 4);
        source.clip = inorganicExplosions[rand];
        source.Play();
    }

    public void PlayRNGGunShot (AudioSource source, float pitch, Vector3 position)
    {
        int rand = Random.Range(0, 1);
        mixer.SetFloat("GunPitch", pitch);
        source.PlayOneShot(gunSounds[rand], .75f);
    }

    public void PlayGunOverheat (AudioSource source)
    {
        source.Stop();
        source.clip = overheatSounds[0];
        mixer.SetFloat("GunPitch", 1f);
        source.Play();
    }

    public void PlayOverheatWarning (AudioSource source, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(overheatSounds[1], position, 1.3f);
    }

    public void PlayRNGCrashNoise (AudioSource source)
    {
        source.Stop();
        source.Play();
    }

    private IEnumerator PlayAfterDelay (AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Play();
    }

}