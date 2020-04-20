using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class PlayerShip : MonoBehaviour
{
    public UnityEngine.UI.Slider speedSlider;
    public UnityEngine.UI.Text speedText;
    public GameObject mouseUI;

    public SpaceshipController stick;

    private float curPitch;
    private float curYaw;

    public float mouseSensitivity = 0.01f;

    void Start()
    {
        stick = GetComponent<SpaceshipController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleThrottleInput();
        HandleRotationInput();

        if (Input.GetKeyDown("p"))
        {
            SceneController.instance.LoseGame();
        }
        else if (Input.GetKeyDown("o"))
        {
            SceneController.instance.WinGame();
        }
    }

    private void HandleThrottleInput()
    {
        stick.ThrottleInput = Input.GetAxis("Throttle");
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

        float effectivePitch = Input.GetAxis("Pitch");
        if (effectivePitch == 0)
        {
            effectivePitch = curPitch;
        }
        else
        {
            curPitch = 0;
        }
        float effectiveYaw = Input.GetAxis("Yaw");
        if (effectiveYaw == 0)
        {
            effectiveYaw = curYaw;
        }
        else
        {
            curYaw = 0;
        }
        float effectiveRoll = Input.GetAxis("Roll");

        EasingFunction.Ease easeType = EasingFunction.Ease.EaseInQuad;
        EasingFunction.Function easeFunction = EasingFunction.GetEasingFunction(easeType);
        float smoothedPitch = Mathf.Sign(effectivePitch) * easeFunction(0, 1, Mathf.Abs(effectivePitch));
        float smoothedYaw = Mathf.Sign(effectiveYaw) * easeFunction(0, 1, Mathf.Abs(effectiveYaw));
        float smoothedRoll = Mathf.Sign(effectiveRoll) * easeFunction(0, 1, Mathf.Abs(effectiveRoll));

        stick.StickInput = new Vector3(smoothedPitch, smoothedYaw, smoothedRoll);

        mouseUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(effectiveYaw * 100, effectivePitch * -100);
    }
}
