using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhaleUpgradeMenu : MonoBehaviour
{
    public Text organicsText;
    public Text inorganicsText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Header").GetComponent<Text>().text += PlayerPrefs.GetString("whale_name");
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpgradeThing(string up)
    {
        Debug.Log(up);
        //Debug.Log(cost);
        // if (WhaleStats.instance.UpgradeLevel(up))
        // {
        //     //Successful upgrade color change or some shit
        // }
        // else
        // {
        //     //Cannot upgrade full level sad noise :(
        // }
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
