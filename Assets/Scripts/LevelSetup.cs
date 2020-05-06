using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (SceneManager.GetActiveScene().name == "IntroCutscene")
        {
            RailIndex = 2;
        }
        else
        {
            RailIndex = Random.Range(0, RailPoints.Length - 1);
            Whale.transform.position = RailPoints[RailIndex].position;
            Whale.transform.rotation = Quaternion.FromToRotation(RailPoints[RailIndex].position, RailPoints[RailIndex + 1].position);
            Player.transform.position = Whale.transform.GetChild(0).transform.position;
            Player.transform.rotation = Whale.transform.rotation;

            Spawner sp = gameObject.GetComponent<Spawner>();
            sp.Waves = WhaleStats.instance.Waves;
            Debug.Log(WhaleStats.instance.Waves.Count);
            sp.Waves.Add(sp.Waves[sp.Waves.Count - 1]);
            for (int i = 0; i < sp.Waves.Count; i++)
            {
                sp.Waves[i] += Random.Range(i, i+2);
            }
            sp.SpawnTutBot();

            Whale.GetComponent<IInventory>().Organics = WhaleStats.instance.Organics;
            Whale.GetComponent<IInventory>().Mechanicals = WhaleStats.instance.Mechanicals;
        }
        //GameObject Whale = Instantiate(WhalePrefab, RailPoints[RailIndex].position, Quaternion.FromToRotation(RailPoints[RailIndex].position, RailPoints[RailIndex + 1].position));
        //GameObject Player = Instantiate(PlayerPrefab, Whale.transform.GetChild(0).transform.position, Whale.transform.rotation);
    }
}
