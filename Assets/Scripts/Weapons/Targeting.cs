using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Targeting : MonoBehaviour
{
    public Transform Target;

    public TargetHud targettingHud;
    public TargetHud closeDebrisHud;

    public InputActionAsset controls;

    public string TargetTag = "Enemy";
    public string DebrisTag = "Lootz";

    private GameObject[] loots;
    private int enemyIndex = 0;

    private void Start()
    {
        loots = GameObject.FindGameObjectsWithTag(TargetTag);
    }

    void Awake ()
    {
        controls.actionMaps[0].FindAction("GetBestTarget", true).performed += ctx => Target = GetBestTarget();
        controls.actionMaps[0].FindAction("GetNextTarget", true).performed += ctx => Target = GetNextTarget();
    }

    private void Update()
    {
        if (Target == null && SceneManager.GetActiveScene().name != "Level1")
        {
            Target = GetNextTarget();
        }

        if (targettingHud != null)
        {
            targettingHud.Target = Target;
        }
        if (closeDebrisHud != null)
        {
            closeDebrisHud.Target = GetClosestDebris();
        }
    }

    private Transform GetClosestDebris()
    {
        loots = GameObject.FindGameObjectsWithTag(DebrisTag);
        float bestTargetVal = float.MaxValue;
        Transform bestTarget = null;
        for (int i = 0; i < loots.Length; i++)
        {
            float distance = Vector3.Distance(loots[i].transform.position, transform.position);
            if (distance < bestTargetVal)
            {
                bestTargetVal = distance;
                bestTarget = loots[i].transform;
                enemyIndex = i;
            }
        }

        return bestTarget;
    }

    private Transform GetBestTarget()
    {
        loots = GameObject.FindGameObjectsWithTag(TargetTag);
        float bestTargetVal = float.MinValue;
        Transform bestTarget = null;
        for (int i = 0; i < loots.Length; i++)
        {
            float forwardNess = Vector3.Dot((loots[i].transform.position - transform.position).normalized, transform.forward);
            if (forwardNess > bestTargetVal)
            {
                bestTargetVal = forwardNess;
                bestTarget = loots[i].transform;
                enemyIndex = i;
            }
        }

        return bestTarget;
    }

    private Transform GetNextTarget()
    {
        loots = GameObject.FindGameObjectsWithTag(TargetTag);
        if (loots.Length == 0) return null;

        if (enemyIndex < loots.Length - 1) { enemyIndex++; }
        else { enemyIndex = 0; }

        return loots[enemyIndex].transform;
    }
}
