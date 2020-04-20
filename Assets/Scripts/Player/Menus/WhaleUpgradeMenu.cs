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
            GameObject.Find(up).GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            SFXController.instance.PlayUpgradedSound(sfxAudio);
            GameObject go;
            switch (up)
            {
                case "HealthPool":
                    go = GameObject.Find("Regen");
                    go.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                    go.GetComponent<Button>().interactable = true;
                    break;
                case "Regen":
                    go = GameObject.Find("Decoy");
                    go.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                    go.GetComponent<Button>().interactable = true;
                    break;
                case "Armor":
                    go = GameObject.Find("Tesla");
                    go.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                    go.GetComponent<Button>().interactable = true;
                    break;
                case "Tesla":
                    go = GameObject.Find("Turrets");
                    go.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                    go.GetComponent<Button>().interactable = true;
                    break;
                default:
                    break;
            }
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
