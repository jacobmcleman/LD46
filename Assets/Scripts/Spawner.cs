﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject WhaleRail;

    public GameObject[] EnemyPrefabs;
    public GameObject[] AsteroidPrefabs;

    public int MaxAsteriods;
    public float avoidDistance = 200;
    public int retryAmount = 10;

    public int MapXMin = -1000;
    public int MapXMax = 1000;
    public int MapYMin = -1000;
    public int MapYMax = 1000;
    public int MapZMin = -1000;
    public int MapZMax = 1000;

    public int[] Waves;
    public int CurWave = 0;

    private List<Transform> Avoids = new List<Transform>(); //List of points to avoid spawning near
    public List<GameObject> Asteriods = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CurWave = 0;

        if (AsteroidPrefabs.Length == 0)
        {
            Debug.LogError("No asteroids you dummy");
        }

        //Add all the whale route points to the list of points to avoid spawning near
        foreach (Transform route in WhaleRail.GetComponent<WhaleRail>().RailPoints)
        {
            foreach (Transform point in route)
            {
                Avoids.Add(point);
                Debug.Log("Adding point at: " + point.position);
            }
        }

        int retryCounter = 0;

        //Spawn Asteriods!
        while (Asteriods.Count < MaxAsteriods)
        {
            //Retry counter to make sure we don't spend too long trying to spawn asteriods if there isn't enough room or something
            if (retryCounter > retryAmount)
            {
                Debug.LogWarning("Asteroid spawner has hit the retry Amount, prematurly stopping asteriod spawning");
                break;
            }

            //Make our random point
            Vector3 point = new Vector3(Random.Range(MapXMin, MapXMax), Random.Range(MapYMin, MapYMax), Random.Range(MapZMin, MapZMax));
            bool goody = true;

            foreach (Transform avoid in Avoids)
            {
                //Check if our random point is too close to anything in avoids
                if (Vector3.Distance(avoid.position, point) < avoidDistance)
                {
                    retryCounter += 1;
                    goody = false;
                    break;
                }
            }

            if (goody) //If we're still good then spawn that fucker
            {
                retryCounter = 0;
                SpawnAsteriod(point);
            }
        }

        Debug.Log("Spawned " + Asteriods.Count + " asteriods");
    }

    private void Update()
    {
        if (Enemies.Count == 0 && CurWave >= (Waves.Length - 1))
        {
            //Level won?!
            Debug.Log("That's all folks!!!");
        }
        else if (Enemies.Count == 0)
        {
            SpawnEnemyWave();
        }
    }

    //Spawing the Asterriod
    void SpawnAsteriod(Vector3 point)
    {
        int prefabIndex = Random.Range(0, AsteroidPrefabs.Length); //Select prefab
        Quaternion randRotate = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)); //Make a random rotation
        GameObject roid = Instantiate(AsteroidPrefabs[prefabIndex], point, randRotate); //Instantiate that bitch
        roid.transform.parent = this.transform; //Set it the child this so that we can just collapse all that 
        Asteriods.Add(roid); //Add it to the asteroid list
        Avoids.Add(roid.transform); //Add it to the avoid list
    }

    void SpawnEnemyWave()
    {
        Debug.Log("Spawning Wave: " + (CurWave + 1));
        int retryCounter = 0;
        //Spawn Enemies!
        while (Enemies.Count < Waves[CurWave])
        {
            //Retry counter to make sure we don't spend too long trying to spawn enemies if there isn't enough room or something
            if (retryCounter > retryAmount)
            {
                Debug.LogWarning("Enermy spawner has hit the retry Amount, prematurly stopping enemy spawning");
                break;
            }

            //Make our random point
            Vector3 point = new Vector3(Random.Range(MapXMin, MapXMax), Random.Range(MapYMin, MapYMax), Random.Range(MapZMin, MapZMax));
            bool goody = true;

            foreach (Transform avoid in Avoids)
            {
                //Check if our random point is too close to anything in avoids
                if (Vector3.Distance(avoid.position, point) < (avoidDistance/2))
                {
                    retryCounter += 1;
                    goody = false;
                    break;
                }
            }

            if (goody) //If we're still good then spawn that fucker
            {
                retryCounter = 0;
                SpawnEnemy(point);
            }
        }
    }

    void SpawnEnemy(Vector3 point)
    {
        int prefabIndex = Random.Range(0, EnemyPrefabs.Length); //Select prefab
        Quaternion randRotate = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)); //Make a random rotation
        GameObject roid = Instantiate(EnemyPrefabs[prefabIndex], point, randRotate); //Instantiate that bitch
        roid.transform.parent = this.transform; //Set it the child this so that we can just collapse all that 
        Enemies.Add(roid); //Add it to the enemy list
        Avoids.Add(roid.transform); //Add it to the avoid list
    }
}