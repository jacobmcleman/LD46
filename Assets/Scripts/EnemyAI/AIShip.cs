using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class AIShip : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 approachDirection = Vector3.zero;

    private SpaceshipController shipControls;

    public float acceptableApproachDirError = 0.2f;
    public float largeDeflectionThreshold = 0.5f;
    public float acceptableRollWindow = 25.0f;

    public float rollAttack = 30;
    public float pitchAttack = 20;
    public float yawAttack = 20;

    // Amount of desired deflection at which the throttle input with be -1
    public float rollSlowDown = 180;
    public float pitchSlowDown = 90;
    public float yawSlowDown = 240;

    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }

    public Vector3 ApproachDirection
    {
        get { return approachDirection; }
        set
        {
            if (value == Vector3.zero) approachDirection = value;
            else approachDirection = value.normalized;
        }
    }

    private void Start()
    {
        shipControls = GetComponent<SpaceshipController>();
    }

    private void Update()
    {
        Vector3 toTarget = (targetPosition - transform.position);
        float distanceToTarget = toTarget.magnitude;
        toTarget = toTarget.normalized;

        // If the approach is incorrect, we may not actually trying to be going to the real target yet
        Vector3 realTargetPos = targetPosition;

        // Approach vector is normalized or zero, this is a quick check to see which it is
        if (approachDirection.sqrMagnitude > 0.1f)
        {
            if (Vector3.Dot(approachDirection, toTarget) < (1 - acceptableApproachDirError))
            {
                realTargetPos = targetPosition - (distanceToTarget * approachDirection);
            }
        }

        Vector3 currentForward = transform.forward;
        Vector3 desiredForward = Vector3.Normalize(realTargetPos - transform.position);

        float deflectionAmount = Mathf.Abs(Vector3.Dot(currentForward, desiredForward));

        bool isSignificantDeflection = deflectionAmount > largeDeflectionThreshold;

        float pitch = 0;
        float yaw = 0;
        float roll = 0;
        float throttle = 0;

        if (isSignificantDeflection)
        {
            Debug.LogFormat("Significant deflection needed ({0})", deflectionAmount);

            // For significany deflections the ship should roll to allow the maneuver to be completed with pitch
            Vector3 currentUp = transform.up;
            Vector3 desiredRollUp = Vector3.ProjectOnPlane(desiredForward, currentForward).normalized;

            float neededRollAngle = Mathf.Sign(Vector3.Dot(currentUp, desiredRollUp)) * Vector3.Angle(currentUp, desiredRollUp);
            roll = neededRollAngle / rollAttack;
            throttle -= Mathf.Abs(rollSlowDown / neededRollAngle);

            if (Mathf.Abs(neededRollAngle) < acceptableRollWindow)
            {
                Debug.Log("Up direction matched, pitching");
                Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, transform.right).normalized;
                float neededPitchAngle = Mathf.Sign(Vector3.Dot(currentForward, pitchDesForward)) * Vector3.Angle(currentForward, pitchDesForward);
                pitch = neededPitchAngle / pitchAttack;
                throttle -= Mathf.Abs(pitchSlowDown / neededPitchAngle);
            }
            else
            {
                Debug.LogFormat("More roll needed, current defletion = {0}", neededRollAngle);
            }
        }
        else
        {
            Debug.Log("Minor adjustments only");
            Vector3 yawDesForward = Vector3.ProjectOnPlane(desiredForward, transform.up);
            Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, transform.right);

            float neededYawAngle = Mathf.Sign(Vector3.Dot(yawDesForward, currentForward)) * Vector3.Angle(yawDesForward, currentForward);
            float neededPitchAngle = Mathf.Sign(Vector3.Dot(pitchDesForward, currentForward)) * Vector3.Angle(pitchDesForward, currentForward);

            pitch = neededPitchAngle / pitchAttack;
            yaw = neededYawAngle / yawAttack;

            throttle += 1;
            throttle -= Mathf.Abs(pitchSlowDown / neededPitchAngle);
            throttle -= Mathf.Abs(yawSlowDown / neededYawAngle);
        }

        Debug.LogFormat("AI flight inputs: \nThrottle: {0}\nPitch: {1}\nYaw: {2}\nRoll: {3}", throttle, pitch, yaw, roll);

        shipControls.StickInput = new Vector3(pitch, yaw, roll);
        shipControls.ThrottleInput = throttle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(targetPosition, 1.0f);
        Gizmos.DrawRay(targetPosition, -approachDirection);
    }
}
