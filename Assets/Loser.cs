using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loser : MonoBehaviour
{
    public void Quit ()
    {
        Application.Quit();
    }

    public void BTM ()
    {
        SceneController.instance.ChangeScene("Main Menu");
    }
}
