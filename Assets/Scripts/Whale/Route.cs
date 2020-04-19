using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    //The four points on our bezier curve
    [SerializeField]
    private Transform[] routePoints;

    //Do gizmo??
    public bool doGizmo;

    //Reused to render gizmos so we cn see the line
    private Vector3 gizmosPosition;

    //See line in editor for ez editing
    private void OnDrawGizmos ()
    {
        if (doGizmo == null) doGizmo = true;
        if (!doGizmo) return;
        //Do the bezier math and draw a sphere along set interval 
        for (float i = 0; i <= 1; i += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - i, 3) * routePoints[0].position +
            3 * Mathf.Pow(1 - i, 2) * i * routePoints[1].position + 
            3 * (1 - i) * Mathf.Pow(i, 2) * routePoints[2].position +
            Mathf.Pow(i, 3) * routePoints[3].position;
            Gizmos.DrawSphere(gizmosPosition, 7f);
        }
        //Draw two lines to visualize the outer edges of the curve
        Gizmos.DrawLine(
            new Vector3(routePoints[0].position.x, routePoints[0].position.y, routePoints[0].position.z),
            new Vector3(routePoints[1].position.x, routePoints[1].position.y, routePoints[1].position.z)
        );
        Gizmos.DrawLine(
            new Vector3(routePoints[2].position.x, routePoints[2].position.y, routePoints[2].position.z),
            new Vector3(routePoints[3].position.x, routePoints[3].position.y, routePoints[3].position.z)
        );

    }
}
