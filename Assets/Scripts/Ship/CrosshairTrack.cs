using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTrack : MonoBehaviour
{
    private float curPitch;
    private float curYaw;

    public float AzimuthMinRotation = -35f;
    public float AzimuthMaxRotation = 35f;
    public float maxWeaponAimAdjustRange = 100f;

    public SpaceshipController stick;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /** 
     * Rotates to find crosshair
     */
    private void RotateToCrosshair()
    {
        RaycastHit hit;

        Vector3 forward = stick.StickInput;
        Vector3 toHitPoint = stick.StickInput;

        if(Physics.Raycast(gameObject.transform.position, forward, out hit, maxWeaponAimAdjustRange, ~(1 << 9)))
        {
            toHitPoint = (hit.point - gameObject.transform.position).normalized;
        }
        
        gameObject.transform.forward = toHitPoint;
    }
}
