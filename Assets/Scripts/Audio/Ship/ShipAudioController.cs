using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class ShipAudioController : MonoBehaviour
{
    //Get the four audio sources: throttle up, throttle down, idle, max power
    public AudioSource upSrc;
    public AudioSource downSrc;
    public AudioSource idleSrc;
    public AudioSource maxSrc;

    //Also get the spaceship controller so we can get  speed and stuff
    private SpaceshipController spaceshipController;

    //We use these to make sure we don't start playing a ramp up or ramp down sound while 
    //the same one is already in progress
    private bool playedUp = false;
    private bool playedDown = false;

    void Start ()
    {
        spaceshipController = GetComponent<SpaceshipController>();
    }

    void Update ()
    {
        BalanceIdleMax();
        PlayUpDown();
    }

    //Figure out what percent of max speed we are going and balance idle vs max power tracks accordingly
    private void BalanceIdleMax ()
    {
        //Get current speed
        float speed = spaceshipController.ForwardSpeed;
        //Get current percentage of maximum potential speed, and round down to 1f
        float max = spaceshipController.Throttle;
        //Make sure we don't set the levels too low
        if (max < 0.1f)
        {
            max = 0.07f;
        }
        //Set the max power track volume equal to our current max speed percentage (divided by 2 because was too loud)
        maxSrc.volume = max / 2;
        //Set the idle track equal volume equal to remaining potential speed (divded by 2 because was too loud)
        idleSrc.volume = 1f - (max / 2);
    }

    //Play the thruster up/thruster down sounds
    private void PlayUpDown ()
    {
        //Check if player is currently throttling up with the sound playing
        if (spaceshipController.ThrottleInput > 0f && !upSrc.isPlaying && !playedUp)
        {
            //Denote that sound has been started
            playedUp = true;
            //Fade in the throttle up sound
            StartCoroutine(FadeIn(upSrc, "up"));
            //If we are playing the throttle down sound right now, fade it out
            if (downSrc.isPlaying)
            {
                StartCoroutine(FadeOut(downSrc, "down"));
            }
        }
        //Check if player is currently throwwling down without the sound playing 
        else if (spaceshipController.ThrottleInput < 0f && !downSrc.isPlaying && !playedDown)
        {
            //Denote that sound has been started
            playedDown = true;
            //Fade in the throttle down sound
            StartCoroutine(FadeIn(downSrc, "down"));
            //If we are playing the throttle up sound right now, fade it out
            if (upSrc.isPlaying)
            {
                StartCoroutine(FadeOut(upSrc, "up"));
            }
        
        }
        //Check if player is currently giving no throttle input while a throttle sound is playing
        else if (spaceshipController.ThrottleInput == 0f && (upSrc.isPlaying || downSrc.isPlaying)) 
        {
            //Fade out throttle sounds since no input is being made
            StartCoroutine(FadeOut(upSrc, "up"));
            StartCoroutine(FadeOut(downSrc, "down"));
        }
        //Check if player is holding down button so we don't loop throttle sounds
        else if (   
            //Check if throttle is maxed out in any direction
            (spaceshipController.ThrottleInput == 1f  || spaceshipController.ThrottleInput == -1f) && 
            //Wait for the clip to be almost done playing
            (downSrc.time > downSrc.clip.length - 1f) || 
            (upSrc.time > upSrc.clip.length -1f))
        {
            //Now that the clip is almost done and the throttle is still maxed out, denote that
            //the appropriate sound has been played already so it is not repeated
            if (downSrc.isPlaying) {
                playedDown = true;
            } else if (upSrc.isPlaying) {
                playedUp = true;
            }
        }
    }

    //Fade in an audio source
    private IEnumerator FadeOut (AudioSource source, string dir)
    {
        float FadeTime = 1f;
        //Make sure we start at whatever our current volume level is
        float startVolume = source.volume;
        while (source.volume > 0.01f) {
            source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        //Stop playing after volume is 0
        source.Stop();
        //Reset the flags so that the sound can be played again
        if (dir == "up") 
        {
            playedUp = false;
        } 
        else 
        {
            playedDown = false;
        }
    }

    private IEnumerator FadeIn (AudioSource source, string dir)
    {
        float FadeTime = 0.1f;
        //Make sure we start at 0 volume
        source.volume = 0f;
        source.Play();
        while (source.volume < 1f) {
            source.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
        //Reset the flags so that the sound can be played again
        if (dir == "up") 
        {
            playedUp = false;
        }
        else 
        {
            playedDown = false;
        }
    }
}
