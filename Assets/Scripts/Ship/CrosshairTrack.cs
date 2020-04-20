using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTrack : MonoBehaviour
{
    private float curPitch;
    private float curYaw;

    public GameObject mouseUI;
    public float AzimuthMinRotation = -35f;
    public float AzimuthMaxRotation = 35f;
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
        RaycastHit hit;

        Vector3 forward = stick.RotationEulers;
        Vector3 toHitPoint = stick.RotationEulers;

        // if(Physics.Raycast(gameObject.transform.position, forward, out hit, maxWeaponAimAdjustRange, ~(1 << 9)))
        // {
        //     toHitPoint = (hit.point - gameObject.transform.position).normalized;
        // }
        // Debug.Log(toHitPoint);
        Vector3 screenPoint = mouseUI.GetComponent<RectTransform>().anchoredPosition;
        toHitPoint = camera.transform.position - camera.ScreenToWorldPoint(screenPoint);
        gameObject.transform.forward = toHitPoint;
    }
}
