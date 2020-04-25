using System.Collections;
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
<<<<<<< HEAD

    public InputActionAsset controls;

    public float mouseSensitivity = 0.01f;

    void Awake ()
    {   
        controls.actionMaps[0].FindAction("Throttle", true).performed += ctx => stick.ThrottleInput = ctx.ReadValue<float>();
        controls.actionMaps[0].FindAction("Pitch", true).performed += ctx => curPitch = ctx.ReadValue<float>();
        controls.actionMaps[0].FindAction("Yaw", true).performed += ctx => curYaw = ctx.ReadValue<float>();
        controls.actionMaps[0].FindAction("Roll", true).performed += ctx => curRoll = ctx.ReadValue<float>();
=======

    public float mouseSensitivity = 0.01f;

    private Controls controls;

    void Awake ()
    {
        controls = new Controls();
        controls.PlayerControls.Throttle.performed += ctx => stick.ThrottleInput = ctx.ReadValue<float>();
        controls.PlayerControls.Pitch.performed += ctx => curPitch = ctx.ReadValue<float>();
        controls.PlayerControls.Yaw.performed += ctx => curYaw = ctx.ReadValue<float>();
        controls.PlayerControls.Roll.performed += ctx => curRoll = ctx.ReadValue<float>();
>>>>>>> 9726370544427b0d99a69202f3a358bc65fea15e
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

    void OnEnable ()
    {
        controls.Enable();
    }

    void OnDisable ()
    {
        controls.Disable();
    }
}
