﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpaceshipController))]
public class AIShip : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 approachDirection = Vector3.zero;

    private SpaceshipController shipControls;

    public float acceptableApproachDirError = 0.2f;
    public float largeDeflectionThreshold = 30.0f;
    public float acceptableRollWindow = 25.0f;

    public float rollAttack = 30;
    public float pitchAttack = 20;
    public float yawAttack = 20;

    // Amount of desired deflection at which the throttle input with be -1
    public float rollSlowDown = 360;
    public float pitchSlowDown = 180;
    public float yawSlowDown = 480;

    public float lookAheadTime = 5.0f;

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
        Vector3 desiredForward = GetIdealDirection();
        SteerTowardsDirection(desiredForward);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(targetPosition, 1.0f);
        Gizmos.DrawRay(targetPosition, -approachDirection);
    }

    private Vector3 GetIdealDirection()
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


        return Vector3.Normalize(realTargetPos - transform.position);
    }

    private void SteerTowardsDirection(Vector3 desiredForward)
    {
        Vector3 currentForward = transform.forward;

        float deflectionAmount = Mathf.Abs(Vector3.Dot(currentForward, desiredForward));
        float angleFromTarget = Vector3.Angle(currentForward, desiredForward);

        bool isSignificantDeflection = angleFromTarget > largeDeflectionThreshold;

        float pitch = 0;
        float yaw = 0;
        float roll = 0;
        float throttle = 0;

        if (isSignificantDeflection)
        {
            // For significany deflections the ship should roll to allow the maneuver to be completed with pitch
            Vector3 currentUp = transform.up;
            Vector3 desiredRollUp = Vector3.ProjectOnPlane(desiredForward, currentForward).normalized;

            float neededRollAngle = Vector3.SignedAngle(currentUp, desiredRollUp, transform.forward);
            roll = -1 * neededRollAngle / rollAttack;
            throttle -= Mathf.Abs(neededRollAngle / rollSlowDown);

            if (Mathf.Abs(neededRollAngle) < acceptableRollWindow)
            {
                Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, transform.right);
                float neededPitchAngle = Vector3.SignedAngle(currentForward, pitchDesForward, transform.right);
                pitch = neededPitchAngle / pitchAttack;
                throttle -= Mathf.Abs(neededPitchAngle / pitchSlowDown);
            }
        }
        else
        {
            Vector3 yawDesForward = Vector3.ProjectOnPlane(desiredForward, transform.up);
            Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, transform.right);

            float neededYawAngle = Vector3.SignedAngle(yawDesForward, currentForward, transform.up);
            float neededPitchAngle = Vector3.SignedAngle(pitchDesForward, currentForward, transform.right);

            pitch = neededPitchAngle / pitchAttack;
            yaw = neededYawAngle / yawAttack;

            throttle += 1;
            throttle -= Mathf.Abs(neededPitchAngle / pitchSlowDown);
            throttle -= Mathf.Abs(neededYawAngle / yawSlowDown);
        }

        shipControls.StickInput = new Vector3(pitch, yaw, roll);
        shipControls.ThrottleInput = throttle;
    }
}