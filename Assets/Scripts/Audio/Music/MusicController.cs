using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Don't kill the jams
    private void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
