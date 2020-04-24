using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settingz : MonoBehaviour
{

    public UnityEngine.Audio.AudioMixer mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public GameObject body;

    void Start ()
    {
        if (PlayerPrefs.HasKey("master_volume"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("master_volume");
        }
        if (PlayerPrefs.HasKey("music_volume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("music_volume");
        }
        if (PlayerPrefs.HasKey("sfx_volume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfx_volume");
        }
    }

    public void HandleMasterChange (float vol)
    {
        mixer.SetFloat("MasterVol", vol);
        PlayerPrefs.SetFloat("master_volume", vol);
    }

    public void HandleMusicChange (float vol)
    {
        mixer.SetFloat("MusicVol", vol);
        PlayerPrefs.SetFloat("music_volume", vol);
    }

    public void HandleSfxChange (float vol)
    {
        mixer.SetFloat("SfxVol", vol);
        PlayerPrefs.SetFloat("sfx_volume", vol);
    }

    public void DisplaySettingsSubMenu (string setting)
    {
        foreach (Transform t in body.transform)
        {
            if (t.gameObject.name == setting)
            {
                t.gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}
