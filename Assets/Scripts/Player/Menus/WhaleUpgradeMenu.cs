using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhaleUpgradeMenu : MonoBehaviour
{
    public Text organicsText;
    public Text inorganicsText;

    private AudioSource sfxAudio;

    // Start is called before the first frame update
    void Start ()
    {
        GameObject.Find("Header").GetComponent<Text>().text += PlayerPrefs.GetString("whale_name");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sfxAudio = GetComponent<AudioSource>();
    }

    void Update ()
    {
        organicsText.text = WhaleStats.instance.Organics.ToString();
        inorganicsText.text = WhaleStats.instance.Mechanicals.ToString();
    }

    public void UpgradeThing(string up)
    {
        if (WhaleStats.instance.UpgradeLevel(up))
        {
            SFXController.instance.PlayUpgradedSound(sfxAudio);
            Debug.Log("Upgraded");
        }
        else
        {
            //Cannot upgrade full level sad noise :(
            Debug.Log("Not Upgraded");
        }
    }

    public void NextLevel()
    {
        SceneController.instance.NextLevel();
    }

    public void FinalLevel()
    {
        SceneController.instance.FinalLevel();
    }
}
