using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleFollow : MonoBehaviour
{
    //All routes we ride hard on mud boggin
    [SerializeField]
    public Transform[] routes;
    
    //Next route to follow
    private int routeToGo;

    //Used in bezier math as iterator
    private float tParam;

    //Where da whale at
    private Vector3 whalePosition;

    //Don't run multiple coroutines bro
    private bool coroutineAllowed;

    public float speedModifier;

    //Prepare to fly
    void Start ()
    {
        //Set iterafor and next route to 0
        routeToGo = 0;
        tParam = 0f;
        //Set speed modifier if it isn't set in editor
        speedModifier = speedModifier == null ? 0.5f : speedModifier;
        //Allow the coroutine to start
        coroutineAllowed = true;
    }

    //Execute flying on next route when the time is right
    void FixedUpdate ()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(Fly(routeToGo));
        }
    }

    private IEnumerator Fly (int routeNumber)
    {
        //Disable extra coroutine calls
        coroutineAllowed = false;
        //Increment through the curve positions
        while (tParam < 1)
        {
            //Set speed
            tParam += Time.deltaTime * speedModifier;
            //Calculator positions
            whalePosition = Mathf.Pow(1 - tParam, 3) * routes[routeNumber].GetChild(0).position +
            3 * Mathf.Pow(1 - tParam, 2) * tParam * routes[routeNumber].GetChild(1).position + 
            3 * (1 - tParam) * Mathf.Pow(tParam, 2) * routes[routeNumber].GetChild(2).position +
            Mathf.Pow(tParam, 3) * routes[routeNumber].GetChild(3).position;
            //Set position and wait for next tick
            transform.position = whalePosition;
            yield return new WaitForEndOfFrame();
        }
        //Reset interator
        tParam = 0f;
        //Update next route index
        routeToGo += 1;
        //If at end of routes, start at beginning again
        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }
        //Allow coroutine again
        coroutineAllowed = true;
    }
}
