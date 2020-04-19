using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShip))]
public class AITestController : MonoBehaviour
{
    public Transform seekPoint;

    void Update()
    {
        GetComponent<AIShip>().TargetPosition = seekPoint.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(seekPoint.position, 1.0f);
    }
}
