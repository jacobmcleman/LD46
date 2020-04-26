using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipController : MonoBehaviour
{
    public bool Bouncer = true; //Enable bouncing on collisions
    public float bounceModifier = 2f;
    public float bounceAngularDrag = 20f;

    public float acceleration = 30.0f;
    public float lateralAcceleration = 20.0f;
    public float verticalAcceleration = 50.0f;
    public float minSpeed = 10.0f;
    public float maxSpeed = 100.0f;

    public float throttleChangeRate = 0.5f;

    // Maximum rate that the vehicle can pitch at (degrees per second)
    public float maxPitchRate = 30.0f;
    public float maxYawRate = 10.0f;
    public float maxRollRate = 45.0f;

    // How many degrees per second each rate 
    public float pitchDelta = 60.0f;
    public float yawDelta = 20.0f;
    public float rollDelta = 90.0f;

    public float steerGravity = 0.1f;

    public float collisionDamageMult = 1.0f;

    private float pitchRate;
    private float yawRate;
    private float rollRate;

    private float oldAD;

    private float throttleValue;

    private Vector3 velocity;

    private Rigidbody rb;
    
    public Vector3 RotationEulers;

    private float throttleInput;
    public float ThrottleInput
    {
        get { return throttleInput; }
        set { throttleInput = Mathf.Clamp(value, -1, 1); }
    }

    public float Throttle
    {
        get { return throttleValue; }
    }

    private Vector3 stickInput;
    // Pitch, Yaw, Roll input in that order
    public Vector3 StickInput
    {
        get { return stickInput; }
        set { stickInput = new Vector3(
            Mathf.Clamp(value.x, -1, 1), 
            Mathf.Clamp(value.y, -1, 1), 
            Mathf.Clamp(value.z, -1, 1)); 
        }
    }

    public float SpeedRatio
    {
        get { return (ForwardSpeed - minSpeed) / (maxSpeed - minSpeed); }
    }

    public float ForwardSpeed
    {
        get { return Vector3.Dot(velocity, transform.forward); }
    }

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public float SlipAmount
    {
        get { return 1 - (Vector3.Dot(transform.forward, Velocity) / 2) + 0.5f;  }
    }

    private void Awake()
    {
        DoDumbConfigChecks();

        throttleValue = 0;
        velocity = Vector3.zero;

        pitchRate = 0.0f;
        yawRate = 0.0f;
        rollRate = 0.0f;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldAD = rb.angularDrag;
    }

    private void DoDumbConfigChecks()
    {
        if (acceleration == 0.0f) Debug.LogWarning("No acceleration set! Ship will not move!");
        if (lateralAcceleration == 0.0f) Debug.LogWarning("No acceleration set! Ship will be super slidey!");
        if (minSpeed > maxSpeed) Debug.LogWarning("Min speed is greater than maxspeed! Throttle will be backwards!");
    }

    private void HandleVelocity()
    {
        float oldThrottle = throttleValue;
        throttleValue += throttleInput * Time.deltaTime * throttleChangeRate;
        throttleValue = Mathf.Clamp01(throttleValue);

        float curTargetSpeed = Mathf.Lerp(minSpeed, maxSpeed, throttleValue);

        Vector3 curVel = velocity;
        Vector3 forwardVel = Vector3.Project(curVel, transform.forward);
        Vector3 vertVel = Vector3.Project(curVel, transform.up);
        Vector3 strafeVel = Vector3.Project(curVel, transform.right);

        float curForwardSpeed = forwardVel.magnitude;
        float forwardSpeedChange = Mathf.MoveTowards(curForwardSpeed, curTargetSpeed, acceleration * Time.deltaTime) - curForwardSpeed;

        float curVerticalSpeed = vertVel.magnitude;
        float verticalSpeedChange = Mathf.MoveTowards(curVerticalSpeed, 0.0f, verticalAcceleration * Time.deltaTime) - curVerticalSpeed;

        float curStrafeSpeed = strafeVel.magnitude;
        float strafeSpeedChange = Mathf.MoveTowards(curStrafeSpeed, 0.0f, lateralAcceleration * Time.deltaTime) - curStrafeSpeed;

        velocity += forwardSpeedChange * transform.forward;
        velocity += verticalSpeedChange * vertVel.normalized;
        velocity += strafeSpeedChange * strafeVel.normalized;

        rb.velocity = velocity;
    }

    
    private void HandleRotation()
    {
        float effectivePitch = StickInput.x;
        float effectiveYaw = StickInput.y;
        float effectiveRoll = StickInput.z;

        pitchRate = Mathf.Clamp((1 - steerGravity) * (pitchRate + (effectivePitch * pitchDelta)), -maxPitchRate, maxPitchRate);
        yawRate = Mathf.Clamp((1 - steerGravity) * (yawRate + (effectiveYaw * yawDelta)), -maxYawRate, maxYawRate);
        rollRate = Mathf.Clamp((1 - steerGravity) * (rollRate + (effectiveRoll * rollDelta)), -maxRollRate, maxRollRate);

        float yawChange = Time.deltaTime * yawRate;
        float pitchChange = Time.deltaTime * pitchRate;
        float rollChange = Time.deltaTime * -rollRate;

        RotationEulers = new Vector3(pitchChange, yawChange, rollChange);

        transform.Rotate(RotationEulers, Space.Self);
    }

    private void OnCollisionEnter(Collision col)
    {
       //SFXController.instance.PlayRNGCrashNoise();
        if (Bouncer)
        {
            Vector3 collisionNormal = col.contacts[0].normal;
            velocity = Vector3.Reflect(velocity * bounceModifier, collisionNormal);
            transform.forward = velocity;
            
            IHealth health = GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(collisionDamageMult * col.relativeVelocity.magnitude, Teams.giantMeteor);
            }
            //StartCoroutine("AngularDragModifier");
        }
    }

    IEnumerator AngularDragModifier()
    {
        rb.angularDrag = bounceAngularDrag;

        yield return new WaitForSeconds(.25f);

        while (rb.angularDrag > oldAD)
        {
            rb.angularDrag -= .1f;
            yield return null;
        }
    }

    void LateUpdate()
    {
        HandleVelocity();
        HandleRotation();
    }
}
