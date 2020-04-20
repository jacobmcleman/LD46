using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleUpgradeMenu : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpgradeThing(WhaleStats.Upgrade up)
    {
        if (WhaleStats.instance.UpgradeLevel(up))
        {
            //Successful upgrade color change or some shit
        }
        else
        {
            //Cannot upgrade full level sad noise :(
        }
    }

    public void NextLevel()
    {
        SceneController.instance.NextLevel();
    }
}
