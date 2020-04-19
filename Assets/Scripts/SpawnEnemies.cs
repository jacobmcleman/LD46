using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public int[] Waves;

    void Start()
    {
        if (EnemyPrefabs.Length == 0)
        {
            Debug.LogError("There are no Enemy Prefabs dummy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
