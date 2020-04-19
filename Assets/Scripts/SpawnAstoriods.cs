using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAstoriods : MonoBehaviour
{
    public GameObject[] AsteroidPrefabs;
    public int MaxAsteriods;
    public float avoidDistance = 25;
    public int retryAmount = 10;

    public int MapXMin = -1000;
    public int MapXMax = 1000;
    public int MapYMin = -1000;
    public int MapYMax = 1000;
    public int MapZMin = -1000;
    public int MapZMax = 1000;

    private List<Transform> Avoids = new List<Transform>(); //List of points to avoid spawning near
    private List<GameObject> Asteriods = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (AsteroidPrefabs.Length == 0)
        {
            Debug.LogError("No asteroids you dummy");
        }

        //Add all the whale route points to the list of points to avoid spawning near
        foreach (Transform route in GameObject.FindGameObjectWithTag("Whale").GetComponent<WhaleFollow>().routes)
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
    }

    void SpawnAsteriod(Vector3 point)
    {
        int prefabIndex = Random.Range(0, AsteroidPrefabs.Length);
        Quaternion randRotate = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
        GameObject roid = Instantiate(AsteroidPrefabs[prefabIndex], point, randRotate);
        roid.transform.parent = this.transform;
        Asteriods.Add(roid);
    }
}
