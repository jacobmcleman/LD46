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

    // Maximum rate that the vehicle can pitch at (degrees per second)
    public float maxPitchRate = 30.0f;
    public float maxYawRate = 10.0f;
    public float maxRollRate = 45.0f;

    // How many degrees per second each rate 
    public float pitchDelta = 60.0f;
    public float yawDelta = 20.0f;
    public float rollDelta = 90.0f;

    public float steerGravity = 0.1f;

    private float pitchRate;
    private float yawRate;
    private float rollRate;

    public float mouseSensitivity = 0.01f;

    private float curPitch;
    private float curYaw;

    private Vector3 velocity;

    private Rigidbody rb;

    public UnityEngine.UI.Slider throttleIndicator;
    public UnityEngine.UI.Slider speedIndicator;
    public UnityEngine.UI.Text speedText;
    public GameObject mouseUI;

    private void Awake()
    {
        DoDumbConfigChecks();

        throttleValue = 0;
        velocity = Vector3.zero;

        curPitch = 0.0f;
        curYaw = 0.0f;

        pitchRate = 0.0f;
        yawRate = 0.0f;
        rollRate = 0.0f;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        throttleIndicator.interactable = false;
        speedIndicator.interactable = false;

        //Cursor.lockState = CursorLockMode.Locked;
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
        //transform.position += velocity * Time.deltaTime;

        speedText.text = "Speed: " + velocity.magnitude;
        throttleIndicator.value = throttleValue;
        speedIndicator.value = (curForwardSpeed - minSpeed) / (maxSpeed - minSpeed);
    }

    private float GetMouseStickAxis(string axis)
    {
        float baseValue = Input.GetAxis(axis);
        return baseValue * mouseSensitivity;
    }

    private void HandleRotation()
    {
        // Process the unity inputs to get the mouse behavior to better behave like a joystick
        float mousePitch = Mathf.Clamp(curPitch + GetMouseStickAxis("MousePitch"), -1, 1);
        float mouseYaw = Mathf.Clamp(curYaw + GetMouseStickAxis("MouseYaw"), -1, 1);

        mouseUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(mouseYaw * 400, mousePitch * -400);
        
        curPitch = Input.GetAxis("Pitch") == 0 ? mousePitch : Input.GetAxis("Pitch");
        curYaw = Input.GetAxis("Yaw") == 0 ? mouseYaw : Input.GetAxis("Yaw");
        float curRoll = Input.GetAxis("Roll");

        pitchRate = Mathf.Clamp((1 - steerGravity) * (pitchRate + (curPitch * pitchDelta)), -maxPitchRate, maxPitchRate);
        yawRate = Mathf.Clamp((1 - steerGravity) * (yawRate + (curYaw * yawDelta)), -maxYawRate, maxYawRate);
        rollRate = Mathf.Clamp((1 - steerGravity) * (rollRate + (curRoll * rollDelta)), -maxRollRate, maxRollRate);

        float yawChange = Time.deltaTime * yawRate;
        float pitchChange = Time.deltaTime * pitchRate;
        float rollChange = Time.deltaTime * -rollRate;

        Vector3 rotationEulers = new Vector3(pitchChange, yawChange, rollChange);

        transform.Rotate(rotationEulers, Space.Self);
    }

    void Update()
    {
        HandleVelocity();
        HandleRotation();
        if (Input.GetKeyDown("p")) {
            SceneController.instance.LoseGame();
        } else if (Input.GetKeyDown("o")) {
            SceneController.instance.WinGame();
        } 
    }
}
