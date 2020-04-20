using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTrack : MonoBehaviour
{
    private float curPitch;
    private float curYaw;

    public GameObject mouseUI;
    public float maxWeaponAimAdjustRange = 100f;

    public SpaceshipController stick;
    private Camera camera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mouseUI = GetComponentInParent<PlayerShip>().mouseUI;
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
        stick = GetComponentInParent<SpaceshipController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateToCrosshair();
    }

    /** 
     * Rotates to find crosshair
     */
    private void RotateToCrosshair()
    {
        Vector2 canvasPoint = mouseUI.GetComponent<Transform>().position;
        //Debug.LogFormat("Mouse at {0}", canvasPoint);
        Vector3 screenPoint = new Vector3(canvasPoint.x, canvasPoint.y, 1);
        Vector3 toHitPoint = camera.transform.position - camera.ScreenToWorldPoint(screenPoint);
        //Debug.Log(toHitPoint);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, toHitPoint, out hit, maxWeaponAimAdjustRange, ~(1 << 9)))
        {
            Debug.LogFormat("Aiming at {0} which is {1} away", hit.collider.gameObject.name, hit.distance);
            toHitPoint = (hit.point - gameObject.transform.position).normalized;
        }

        gameObject.transform.forward = -toHitPoint;
    }
}
