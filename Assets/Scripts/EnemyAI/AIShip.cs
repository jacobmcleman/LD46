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
    public float largeDeflectionThreshold = 30.0f;
    public float acceptableRollWindow = 25.0f;

    public float rollAttack = 30;
    public float pitchAttack = 20;
    public float yawAttack = 20;

    // Amount of desired deflection at which the throttle input with be -1
    public float rollSlowDown = 360;
    public float pitchSlowDown = 180;
    public float yawSlowDown = 480;

    public float magicThreshold = 0.1f;

    public bool doCollisionAvoidance = true;
    public float lookAheadTime = 4.0f;
    public float castRadius = 6.0f;

    public float safeDistanceValue = 2.0f;
    public float onTargetValue = 10.0f;
    public float minimalSteerValue = 1.0f;
    public float targetProxValue = 3.0f;

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
        if(doCollisionAvoidance) desiredForward = LookAheadCheck(desiredForward);
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

    private Vector3 LookAheadCheck(Vector3 desiredForward)
    {
        //if (TestPotentialPath(desiredForward) > shipControls.ForwardSpeed * lookAheadTime) return desiredForward;

        List<KeyValuePair<Vector3, float>> solutionScores = new List<KeyValuePair<Vector3, float>>();
        // Take a look at a few possibilities for where the ship could be in a few seconds
        DoSolutionScoring(desiredForward * shipControls.ForwardSpeed, desiredForward, ref solutionScores);
        //DoSolutionScoring(transform.forward * shipControls.ForwardSpeed, desiredForward, ref solutionScores);
        DoSolutionScoring(-transform.forward * shipControls.ForwardSpeed, desiredForward, ref solutionScores);
        DoSolutionScoring(shipControls.Velocity, desiredForward, ref solutionScores);
        DoSolutionScoring(shipControls.Velocity + (transform.up * 0.5f * shipControls.ForwardSpeed), desiredForward, ref solutionScores);
        DoSolutionScoring(shipControls.Velocity + (-transform.up * 0.5f * shipControls.ForwardSpeed), desiredForward, ref solutionScores);
        DoSolutionScoring(shipControls.Velocity + (transform.right * 0.5f * shipControls.ForwardSpeed), desiredForward, ref solutionScores);
        DoSolutionScoring(shipControls.Velocity + (-transform.right * 0.5f * shipControls.ForwardSpeed), desiredForward, ref solutionScores);

        // Pick the maximum score
        float maxScore = float.MinValue;
        Vector3 bestDirection = desiredForward;

        foreach(KeyValuePair<Vector3, float> solution in solutionScores)
        {
            if(solution.Value > maxScore)
            {
                maxScore = solution.Value;
                bestDirection = solution.Key;
            }
        }

        return bestDirection;
    }

    private float TestPotentialPath(Vector3 testVel)
    {
        Vector3 toTarget = targetPosition - transform.position;

        float distanceToTarget = toTarget.magnitude;
        float curSpeed = testVel.magnitude;

        float castLength = Mathf.Min(distanceToTarget, lookAheadTime * curSpeed);

        RaycastHit hit;
        bool didHit = Physics.SphereCast(transform.position, castRadius, testVel.normalized, out hit, castLength, ~(1 << 9));
        return didHit ? hit.distance : (castLength + castRadius); // Can't guarantee an option is good beyond as far as we looked
    }

    private float ScoreSolution(Vector3 testVel, Vector3 desiredDirection)
    {
        float collisionDistance = TestPotentialPath(testVel);
        float closenessToDesiredDirection = Vector3.Dot(testVel, desiredDirection);
        float steeringEffort = Vector3.Dot(shipControls.Velocity, testVel) + Vector3.Dot(transform.forward, testVel);
        float targetProx = -Vector3.Distance(transform.position + lookAheadTime * testVel, targetPosition) / 1000;

        return (closenessToDesiredDirection * onTargetValue) 
            + (collisionDistance * safeDistanceValue)
            + (targetProx * targetProxValue)
            + (steeringEffort * minimalSteerValue);
    }

    private void DoSolutionScoring(Vector3 testVel, Vector3 desiredDirection, ref List<KeyValuePair<Vector3, float>> solutionScores)
    {
        solutionScores.Add(new KeyValuePair<Vector3, float>(testVel, ScoreSolution(testVel, desiredDirection)));
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
            // For significant deflections the ship should roll to allow the maneuver to be completed with pitch
            Vector3 currentUp = transform.up;
            Vector3 desiredRollUp = Vector3.ProjectOnPlane(desiredForward, currentForward);
            float neededRollAngle = 0;
            if(desiredRollUp.sqrMagnitude > magicThreshold)
            {
                desiredRollUp.Normalize();
                neededRollAngle = Vector3.SignedAngle(currentUp, desiredRollUp, transform.forward);
            }

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
            float neededYawAngle = yawDesForward.sqrMagnitude > magicThreshold ? -Vector3.SignedAngle(yawDesForward, transform.forward, transform.up) : 0;
            yaw = neededYawAngle / yawAttack;
            throttle -= Mathf.Abs(neededYawAngle / yawSlowDown);

            Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, transform.right);
            float neededPitchAngle = pitchDesForward.sqrMagnitude > magicThreshold ? -Vector3.SignedAngle(pitchDesForward, transform.forward, transform.right) : 0;
            pitch = neededPitchAngle / pitchAttack;
            throttle -= Mathf.Abs(neededPitchAngle / pitchSlowDown);
            throttle += 1;
        }

        shipControls.StickInput = new Vector3(pitch, yaw, roll);

        if(shipControls.Throttle + throttle < maxThrottle)
        {
            shipControls.ThrottleInput = throttle;
        }
    }
}
