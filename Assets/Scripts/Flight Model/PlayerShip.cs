﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpaceshipController))]
public class PlayerShip : MonoBehaviour
{
    public UnityEngine.UI.Slider speedSlider;
    public UnityEngine.UI.Text speedText;
    public GameObject mouseUI;

    public SpaceshipController stick;

    private float curPitch;
    private float curYaw;
    private float curRoll;

    public InputActionAsset controls;
    private InputAction throttle;
    private InputAction pitch;
    private InputAction yaw;
    private InputAction roll;

    public float mouseSensitivity = 0.01f;


    void Awake ()
    {   
        throttle = controls.actionMaps[0].FindAction("Throttle", true);
        pitch = controls.actionMaps[0].FindAction("Pitch", true);
        yaw = controls.actionMaps[0].FindAction("Yaw", true);
        roll = controls.actionMaps[0].FindAction("Roll", true);
        
        throttle.performed += ThrottleAction;
        pitch.performed += PitchAction;
        yaw.performed += YawAction;
        roll.performed += RollAction;
    }

    void OnDisable ()
    {
        throttle.performed -= ThrottleAction;
        pitch.performed -= PitchAction;
        yaw.performed -= YawAction;
        roll.performed -= RollAction;
    }

    void ThrottleAction (InputAction.CallbackContext ctx)
    {
        stick.ThrottleInput = ctx.ReadValue<float>();
    }

    void PitchAction (InputAction.CallbackContext ctx)
    {
        curPitch = ctx.ReadValue<float>();
    }

    void YawAction (InputAction.CallbackContext ctx)
    {
        curYaw = ctx.ReadValue<float>();
    }

    void RollAction (InputAction.CallbackContext ctx)
    {
        curRoll = ctx.ReadValue<float>();
    }

    void Start()
    {
        stick = GetComponent<SpaceshipController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotationInput();
    }

    private float GetMouseStickAxis(string axis)
    {
        float baseValue = Input.GetAxis(axis);
        return baseValue * mouseSensitivity;
    }

    private void HandleRotationInput()
    {
        // Process the unity inputs to get the mouse behavior to better behave like a joystick
         curPitch = Mathf.Clamp(curPitch + GetMouseStickAxis("MousePitch"), -1, 1);
         curYaw = Mathf.Clamp(curYaw + GetMouseStickAxis("MouseYaw"), -1, 1);

        EasingFunction.Ease easeType = EasingFunction.Ease.EaseInQuad;
        EasingFunction.Function easeFunction = EasingFunction.GetEasingFunction(easeType);
        float smoothedPitch = Mathf.Sign(curPitch) * easeFunction(0, 1, Mathf.Abs(curPitch));
        float smoothedYaw = Mathf.Sign(curYaw) * easeFunction(0, 1, Mathf.Abs(curYaw));
        float smoothedRoll = Mathf.Sign(curRoll) * easeFunction(0, 1, Mathf.Abs(curRoll));

        stick.StickInput = new Vector3(smoothedPitch, smoothedYaw, smoothedRoll);

        mouseUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(curYaw * 100, curPitch * -100);
    }

}
