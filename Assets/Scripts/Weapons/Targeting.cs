using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Targeting : MonoBehaviour
{
    public Transform Target;

    public string TargetTag = "Enemy";

    private GameObject[] enemies;
    private int enemyIndex = 0;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag(TargetTag);
    }

    private void Update()
    {
        if (Input.GetButtonDown("GetBestTarget"))
        {
            Target = GetBestTarget();
        }
        if (Input.GetButtonDown("GetNextTarget"))
        {
            Target = GetNextTarget();
        }
        if (Target == null && SceneManager.GetActiveScene().name != "Level1")
        {
            Target = GetNextTarget();
        }
    }

    private Transform GetBestTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(TargetTag);
        float bestTargetVal = float.MinValue;
        Transform bestTarget = null;
        for (int i = 0; i < enemies.Length; i++)
        {
            float forwardNess = Vector3.Dot((enemies[i].transform.position - transform.position).normalized, transform.forward);
            if (forwardNess > bestTargetVal)
            {
                bestTargetVal = forwardNess;
                bestTarget = enemies[i].transform;
                enemyIndex = i;
            }
        }

        return bestTarget;
    }

    private Transform GetNextTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(TargetTag);
        if (enemyIndex < enemies.Length) { enemyIndex++; }
        else { enemyIndex = 0; }
        //Debug.Log("Enemy Index: " + enemyIndex);
        return enemies[enemyIndex].transform;
    }
}
