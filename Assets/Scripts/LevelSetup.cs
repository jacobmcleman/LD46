using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    //public GameObject WhalePrefab;
    //public GameObject PlayerPrefab;
    public GameObject WhaleRail;
    public GameObject Player;
    public GameObject Whale;

    public int RailIndex;

    private Transform[] RailPoints;

    private void Awake()
    {
        RailPoints = WhaleRail.GetComponent<WhaleRail>().RailPoints;

        RailIndex = Random.Range(0, RailPoints.Length - 1);
        //GameObject Whale = Instantiate(WhalePrefab, RailPoints[RailIndex].position, Quaternion.FromToRotation(RailPoints[RailIndex].position, RailPoints[RailIndex + 1].position));
        //GameObject Player = Instantiate(PlayerPrefab, Whale.transform.GetChild(0).transform.position, Whale.transform.rotation);
        Whale.transform.position = RailPoints[RailIndex].position;
        Whale.transform.rotation = Quaternion.FromToRotation(RailPoints[RailIndex].position, RailPoints[RailIndex + 1].position);
        Player.transform.position = Whale.transform.GetChild(0).transform.position;
        Player.transform.rotation = Whale.transform.rotation;

        Spawner sp = gameObject.GetComponent<Spawner>();
        sp.Waves.Add(sp.Waves[sp.Waves.Count - 1]);
        for (int i = 0; i < sp.Waves.Count; i++)
        {
            sp.Waves[i] += Random.Range(0, 2);
        }

        Whale.GetComponent<IInventory>().Organics = WhaleStats.instance.Organics;
        Whale.GetComponent<IInventory>().Mechanicals = WhaleStats.instance.Mechanicals;
    }
}
