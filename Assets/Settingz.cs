using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settingz : MonoBehaviour
{

    public UnityEngine.Audio.AudioMixer mixer;

    public Slider[] sliderz;

    public void HandleMasterChange (float vol)
    {
        mixer.SetFloat("MasterVol", vol);
    }

    public void HandleMusicChange (float vol)
    {
        mixer.SetFloat("MusicVol", vol);
    }

    public void HandleSfxChange (float vol)
    {
        mixer.SetFloat("SfxVol", vol);
    }
}
