using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class PlayerShip : MonoBehaviour
{
    public UnityEngine.UI.Slider throttleIndicator;
    public UnityEngine.UI.Slider speedIndicator;
    public UnityEngine.UI.Text speedText;
    public GameObject mouseUI;

    private SpaceshipController stick;

    void Start()
    {
        stick = GetComponent<SpaceshipController>();
    }

    void Update()
    {
        HandleThrottleInput();
        HandleRotationInput();
    }

    private void HandleThrottleInput()
    {
        stick.ThrottleInput = Input.GetAxis("Throttle");
    }


    private void HandleRotationInput()
    {

    }
}
