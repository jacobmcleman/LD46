using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    private Vector3 stickInput;
    private float throttle;

    [Tooltip("How powerfully the plane can maneuver in each axis.\n\nX: Pitch\nY: Yaw\nZ: Roll")]
    public Vector3 turnTorques = new Vector3(60.0f, 10.0f, 90.0f);
    [Tooltip("Torque used by the magic banking force that rotates the plane when the plane is banked.")]
    public float bankTorque = 5.0f;
    [Tooltip("Power of the engine at max throttle.")]
    public float maxThrust = 3000.0f;
    [Tooltip("How quickly the jet can accelerate and decelerate.")]
    public float acceleration = 10.0f;
    [Tooltip("How quickly the jet will brake when the throttle goes below neutral.")]
    public float brakeDrag = 5.0f;

    public float Pitch
    {
        get { return stickInput.x; }
        set { stickInput.x = value; }
    }
    public float Roll
    {
        get { return stickInput.z; }
        set { stickInput.z = value; }
    }
    public float Yaw
    {
        get { return stickInput.y; }
        set { stickInput.y = value; }
    }

    // Pitch, Yaw, and Roll represented as a Vector3, in that order.
    public Vector3 Combined
    {
        get { return stickInput; }
        set { stickInput = value; }
    }
    public float Throttle
    {
        get { return throttle; }
        set { throttle = Mathf.Clamp(value, ThrottleMin, ThrottleMax); }
    }

    public const float ThrottleNeutral = 0.33f;
    public const float ThrottleMin = 0.1f;
    public const float ThrottleMax = 1f;
    public const float ThrottleSpeed = 2f;

    private Rigidbody rb;

    private float throttleTrue = ThrottleNeutral;

    private const float FORCE_MULT = 100.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // I just... really don't want to deal with offset COM
        rb.centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        
    }
}
