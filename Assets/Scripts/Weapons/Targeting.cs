using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public Transform Target;

    public TargetHud targettingHud;

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
        if (Target == null)
        {
            Target = GetNextTarget();
        }

        if(targettingHud != null)
        {
            targettingHud.Target = Target;
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
        if (enemyIndex < enemies.Length - 1) { enemyIndex++; }
        else { enemyIndex = 0; }
        Debug.Log("Enemy Index: " + enemyIndex);
        return enemies[enemyIndex].transform;
    }
}
