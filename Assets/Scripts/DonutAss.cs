using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonutAss : MonoBehaviour
{
   public float Fill
    {
        get
        {
            return curT_;
        }
        set 
        {
            curT_ = value;
            progressBarUI_.fillAmount = curT_;
        }
    }
    
    private float curT_ = 0.0f;

    public Image progressBarUI_;

    // Start is called before the first frame update
    void Start()
    {
        float curT_ = 0.0f;

        progressBarUI_.fillAmount = curT_;
    }
}
