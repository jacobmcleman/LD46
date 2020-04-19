using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleRail : MonoBehaviour
{
    public bool doGizmo = true;

    [SerializeField]
    public Transform[] RailPoints;

    private void Start()
    {

    }

    private void OnDrawGizmos()
    {
        if (!doGizmo) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < RailPoints.Length; i++)
        {
            Gizmos.DrawSphere(RailPoints[i].position, 10f);

            if (i > 0)
            {
                Gizmos.DrawLine(RailPoints[i - 1].position, RailPoints[i].position);
            }
        }
    }
}
