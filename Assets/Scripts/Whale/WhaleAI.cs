using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour
{
    public GameObject WhaleRail;
    private Transform[] RailPoints;
    private AIShip flightController;

    private int curPoint = 0;
    public int acceptableDistance = 10;

    private Transform WhaleMouth;

    // Start is called before the first frame update
    void Start()
    {
        RailPoints = WhaleRail.GetComponent<WhaleRail>().RailPoints;
        flightController = gameObject.GetComponent<AIShip>();
        WhaleMouth = GameObject.FindGameObjectWithTag("WhaleMouth").transform;

        curPoint = GameObject.FindGameObjectWithTag("Spawner").GetComponent<LevelSetup>().RailIndex + 1;
        flightController.TargetPosition = RailPoints[curPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(WhaleMouth.position, RailPoints[curPoint].position) < acceptableDistance)
        {          
            Debug.Log("Whale at " + RailPoints[curPoint].name + " now going to " + RailPoints[curPoint + 1]);

            curPoint++;
            //Loop check
            if (curPoint == RailPoints.Length) { curPoint = 0; } 

            flightController.TargetPosition = RailPoints[curPoint].position;
        }
    }
}
