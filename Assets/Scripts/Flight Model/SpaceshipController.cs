using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipController : MonoBehaviour
{
    public float acceleration = 30.0f;
    public float lateralAcceleration = 20.0f;
    public float verticalAcceleration = 50.0f;
    public float minSpeed = 10.0f;
    public float maxSpeed = 100.0f;

    public float throttleChangeRate = 0.5f;
    // Value from 0-1 where 0 is trying to reach minSpeed and 1 is trying to reach maxSpeed
    private float throttleValue;

    public float pitchRate = 30.0f;
    public float yawRate = 10.0f;
    public float rollRate = 45.0f;

    public float mouseSensitivity = 0.01f;

    private float curPitch;
    private float curYaw;

    private Vector3 velocity;

    private Rigidbody rb;

    public UnityEngine.UI.Slider throttleIndicator;
    public UnityEngine.UI.Slider speedIndicator;
    public UnityEngine.UI.Text speedText;

    void Start()
    {
        DoDumbConfigChecks();

        throttleValue = 0;
        velocity = Vector3.zero;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        throttleIndicator.interactable = false;
        speedIndicator.interactable = false;

        curPitch = 0.0f;
        curYaw = 0.0f;

        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void DoDumbConfigChecks()
    {
        if (acceleration == 0.0f) Debug.LogWarning("No acceleration set! Ship will not move!");
        if (lateralAcceleration == 0.0f) Debug.LogWarning("No acceleration set! Ship will be super slidey!");
        if (minSpeed > maxSpeed) Debug.LogWarning("Min speed is greater than maxspeed! Throttle will be backwards!");
        if (rb == null) Debug.LogWarning("No rigidbody on spaceship!");
    }

    private void HandleVelocity()
    {
        float throttleChange = Input.GetAxis("Throttle");
        throttleValue += throttleChange * Time.deltaTime * throttleChangeRate;
        throttleValue = Mathf.Clamp01(throttleValue);

        float curTargetSpeed = Mathf.Lerp(minSpeed, maxSpeed, throttleValue);

        Vector3 curVel = velocity;
        Vector3 forwardVel = Vector3.Project(curVel, transform.forward);
        Vector3 vertVel = Vector3.Project(curVel, transform.up);
        Vector3 strafeVel = Vector3.Project(curVel, transform.right);

        float curForwardSpeed = Vector3.Dot(transform.forward, curVel);
        float forwardSpeedChange = Mathf.MoveTowards(curForwardSpeed, curTargetSpeed, acceleration * Time.deltaTime) - curForwardSpeed;

        float curVerticalSpeed = vertVel.magnitude;
        float verticalSpeedChange = Mathf.MoveTowards(curVerticalSpeed, 0.0f, verticalAcceleration * Time.deltaTime) - curVerticalSpeed;

        float curStrafeSpeed = strafeVel.magnitude;
        float strafeSpeedChange = Mathf.MoveTowards(curStrafeSpeed, 0.0f, lateralAcceleration * Time.deltaTime) - curStrafeSpeed;

        velocity += forwardSpeedChange * transform.forward;
        velocity += verticalSpeedChange * vertVel.normalized;
        velocity += strafeSpeedChange * strafeVel.normalized;

        rb.velocity = velocity;
        //transform.position += velocity * Time.deltaTime;

        speedText.text = "Speed: " + velocity.magnitude;
        throttleIndicator.value = throttleValue;
        speedIndicator.value = (curForwardSpeed - minSpeed) / (maxSpeed - minSpeed);
    }

    private void HandleRotation()
    {
        curPitch = Input.GetAxis("Pitch") == 0 ? Mathf.Clamp(curPitch + Input.GetAxis("MousePitch") * mouseSensitivity, -1, 1) : Input.GetAxis("Pitch");
        curYaw = Input.GetAxis("Yaw") == 0 ? Mathf.Clamp(curYaw + Input.GetAxis("MouseYaw") * mouseSensitivity, -1, 1) : Input.GetAxis("Yaw");

        float yawChange = curYaw * Time.deltaTime * yawRate;
        float pitchChange = curPitch * Time.deltaTime * pitchRate;
        float rollChange = Input.GetAxis("Roll") * Time.deltaTime * -rollRate;

        Vector3 rotationEulers = new Vector3(pitchChange, yawChange, rollChange);

        transform.Rotate(rotationEulers, Space.Self);
    }

    void Update()
    {
        HandleVelocity();
        HandleRotation();
    }
}
