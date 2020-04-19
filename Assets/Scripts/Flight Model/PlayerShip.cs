using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class PlayerShip : MonoBehaviour
{
    public UnityEngine.UI.Slider speedSlider;
    public UnityEngine.UI.Text speedText;
    public GameObject mouseUI;

    private SpaceshipController stick;

    private float curPitch;
    private float curYaw;

    public float mouseSensitivity = 0.01f;

    void Start()
    {
        _stick = GetComponent<SpaceshipController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleThrottleInput();
        HandleRotationInput();
        DoHud();

        if (Input.GetKeyDown("p"))
        {
            SceneController.instance.LoseGame();
        }
        else if (Input.GetKeyDown("o"))
        {
            SceneController.instance.WinGame();
        }
    }

    private void DoHud()
    {
        speedText.text = ((int)stick.ForwardSpeed).ToString();
        speedSlider.value = stick.SpeedRatio;
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

        stick.StickInput = new Vector3(effectivePitch, effectiveYaw, effectiveRoll);

        mouseUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(effectiveYaw * 100, effectivePitch * -100);
    }
}
