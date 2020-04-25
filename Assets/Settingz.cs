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

    public Slider pitchSens;
    public Slider rollSens;
    public Slider yawSens;
    public Slider mouseSens;

    public GameObject body;

    public string[] floatSettingsKeys;

    private bool init = false;

    void Start ()
    {
        if (PlayerPrefs.HasKey("MasterVol"))  masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        if (PlayerPrefs.HasKey("MusicVol")) musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        if (PlayerPrefs.HasKey("SfxVol")) sfxSlider.value = PlayerPrefs.GetFloat("SfxVol");
        if (PlayerPrefs.HasKey("pitch_sensitivity")) pitchSens.value = PlayerPrefs.GetFloat("pitch_sensitivity");
        if (PlayerPrefs.HasKey("roll_sensitivity")) rollSens.value = PlayerPrefs.GetFloat("roll_sensitivity");
        if (PlayerPrefs.HasKey("yaw_sensitivity")) yawSens.value = PlayerPrefs.GetFloat("yaw_sensitivity");
        if (PlayerPrefs.HasKey("mouse_sensitivity")) mouseSens.value = PlayerPrefs.GetFloat("mouse_sensitivity");
        foreach (string key in floatSettingsKeys)
        {
            GameObject.Find(key).GetComponent<Slider>().onValueChanged.AddListener(value => HandleFloatSettingChange(key, value));
        }
        foreach (Transform t in gameObject.transform)
        {
            if (t.gameObject.name == "Body")
            {
                foreach (Transform t2 in t.gameObject.transform)
                {
                    if (t2.gameObject.name != "Audio")
                    {
                        t2.gameObject.SetActive(false);
                    }
                }
                continue;
            }
        }
        this.gameObject.SetActive(false);
    }

    public void HandleFloatSettingChange (string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        if (key == "MasterVol" || key == "MusicVol" || key == "SfxVol")
        {
            mixer.SetFloat(key, value);
        }
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
