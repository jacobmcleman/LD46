using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    public AudioClip[] beeps;
    public AudioClip[] cargoLauncherSounds;
    public AudioClip[] crashSounds;
    public AudioClip[] gunSounds;
    public AudioClip[] organicExplosions;
    public AudioClip[] inorganicExplosions;
    public AudioClip[] blipSounds;

    public AudioClip hitmarkerSound;
    public AudioClip cooldownSound;
    public AudioClip confirmedKillSound;
    public AudioClip toolTipSound;
    public AudioClip resourcePickupSound;
    public AudioClip upgradePurchasedSound;
    public AudioClip overheatCooldownSound;
    public AudioClip overheatWarningSound;
    public AudioClip missileLaunchSound;

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

    void Start ()
    {
        if(PlayerPrefs.HasKey("sfx_volume"))
        {
            mixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("sfx_volume"));
        }
    }

    public void PlayToolTipSFX (AudioSource source)
    {
        source.Stop();
        source.clip = toolTipSound;
        source.Play();
    }

    public void PlayResourcePickupSFX (AudioSource source)
    {
        source.Stop();
        source.clip = resourcePickupSound;
        source.Play();

    }

    public void PlayRocketLaunchCooldownSFX (AudioSource source)
    {
        source.PlayOneShot(missileLaunchSound, 1f);
        StartCoroutine(PlayAfterDelay(source, cooldownSound, 1f, .1f));
    }

    public void PlayRocketExplosionSFX (AudioSource source)
    {
        source.Stop();
        int rand = Random.Range(0, 4);
        source.clip = inorganicExplosions[rand];
        source.Play();
    }

    public void PlayUpgradedSound (AudioSource source)
    {
        source.PlayOneShot(upgradePurchasedSound, 1f);
    }

    public void PlayRNGCrashNoise (AudioSource source)
    {
        //Sometimes we crash into a dead enemy with no existing audio source anymore
        //so I check if it's null
        //this is probably bad?? idk but if this was javascript this wouldn't be good
        if (source == null) return;
        int rand = Random.Range(0, crashSounds.Length);
        source.PlayOneShot(crashSounds[rand], 4f);
    }

    public void PlayHitmarkerSound (AudioSource source, float pitch)
    {   
        mixer.SetFloat("HitmarkerPitch", pitch);
        Debug.Log(pitch);
        source.PlayOneShot(hitmarkerSound, 1.5f);
    }

    public void PlayKillConfirmedSound (AudioSource source)
    {
        mixer.SetFloat("HitmarkerPitch", 1f);
        source.PlayOneShot(confirmedKillSound, 3f);
    }

    public void PlayNewTargetSound (AudioSource source)
    {
        int rand = Random.Range(0, blipSounds.Length);
        source.PlayOneShot(blipSounds[rand], 2f);
    }

    public void PlayPlayerDamagedSound (AudioSource source)
    {
        source.PlayOneShot(beeps[9], 2f);
    }

    public void PlayPlayerDeathSound (AudioSource source)
    {
        source.PlayOneShot(beeps[10], 3f);
    }

    public void PlayRandomDeathSound (AudioSource source, EnemyType type)
    {
        AudioClip[] deathSounds = type == EnemyType.Organic ? organicExplosions : inorganicExplosions;
        int rand = Random.Range(0, deathSounds.Length);
        source.PlayOneShot(deathSounds[rand], 2f);
    }

    public void PlayRNGGunShot (AudioSource source, float pitch, Vector3 position)
    {
        int rand = Random.Range(0, 1);
        mixer.SetFloat("GunPitch", pitch);
        source.PlayOneShot(gunSounds[rand], .5f);
    }

    public void PlayGunOverheat (AudioSource source)
    {
        source.Stop();
        source.clip = overheatCooldownSound;
        mixer.SetFloat("GunPitch", 1f);
        source.Play();
    }

    public void PlayOverheatWarning (AudioSource source, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(overheatWarningSound, position, 1.3f);
    }

    private IEnumerator PlayAfterDelay (AudioSource source, AudioClip clip, float delay, float vol)
    {
        yield return new WaitForSeconds(delay);
        source.PlayOneShot(clip, vol);
    }

}
