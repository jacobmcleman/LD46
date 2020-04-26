using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

[RequireComponent(typeof(SpaceshipController))]
public class AIShip : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 approachDirection = Vector3.zero;

    private SpaceshipController shipControls;
    
    public float maxThrottle = 1.0f;

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

    private float magicThreshold = 0.1f;

    public bool doCollisionAvoidance = true;
    public float lookAheadTime = 4.0f;
    public float castRadius = 6.0f;

    public float safeDistanceValue = 2.0f;
    public float onTargetValue = 10.0f;
    public float minimalSteerValue = 1.0f;
    public float targetProxValue = 3.0f;

    private JobHandle steeringJobHandle;
    bool firstFrame;

    private Vector3 lastTargetDirection;

    private struct SteeringNativeData
    {
        public NativeArray<SpherecastCommand> castCommands;
        public NativeArray<RaycastHit> castResults;
        public NativeArray<Vector3> nativeSteeringOptions;
        public NativeArray<float> scores;
        public NativeArray<Vector3> bestSolution;
        public NativeArray<float> controlValues;
    }
    private SteeringNativeData steeringNativeData;

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
        firstFrame = true;

        steeringNativeData.bestSolution = new NativeArray<Vector3>(1, Allocator.Persistent);
        steeringNativeData.controlValues = new NativeArray<float>(4, Allocator.Persistent);

        lastTargetDirection = transform.forward;
    }

    private void Update()
    {
        if(firstFrame)
        {
            firstFrame = false;
        }
        else
        {
            steeringJobHandle.Complete();
            float pitch = steeringNativeData.controlValues[0];
            float yaw = steeringNativeData.controlValues[1];
            float roll = steeringNativeData.controlValues[2];
            float throttle = steeringNativeData.controlValues[3];

            shipControls.StickInput = new Vector3(pitch, yaw, roll);

            if (shipControls.Throttle + throttle < maxThrottle)
            {
                shipControls.ThrottleInput = throttle;
            }

            lastTargetDirection = steeringNativeData.bestSolution[0];

            FreeEphemeralNativeData();
        }

        Vector3 desiredForward = GetIdealDirection();
        JobHandle dependent;

        if (doCollisionAvoidance)
        {
            dependent = LookAheadCheck(desiredForward, ref steeringNativeData);
        }
        else
        {
            steeringNativeData.bestSolution[0] = desiredForward;
            dependent = default;
        }

        GetSteerInputJob getSteerInputJob = new GetSteerInputJob();
        getSteerInputJob.desiredForwardNative = steeringNativeData.bestSolution;
        getSteerInputJob.controlValues = steeringNativeData.controlValues;
        getSteerInputJob.currentForward = transform.forward;
        getSteerInputJob.currentUp = transform.up;
        getSteerInputJob.currentRight = transform.right;
        getSteerInputJob.rollAttack = rollAttack;
        getSteerInputJob.rollSlowDown = rollSlowDown;
        getSteerInputJob.acceptableRollWindow = acceptableRollWindow;
        getSteerInputJob.pitchAttack = pitchAttack;
        getSteerInputJob.pitchSlowDown = pitchSlowDown;
        getSteerInputJob.yawAttack = yawAttack;
        getSteerInputJob.yawSlowDown = yawSlowDown;
        getSteerInputJob.largeDeflectionThreshold = largeDeflectionThreshold;
        getSteerInputJob.magicThresholdValue = magicThreshold;

        steeringJobHandle = getSteerInputJob.Schedule(dependent);
    }

    private void OnDestroy()
    {
        // Need to complete all the jobs before it is safe to free the data
        steeringJobHandle.Complete();
        FreeAllNativeData();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(targetPosition, 1.0f);
        Gizmos.DrawRay(targetPosition, -approachDirection);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, targetPosition);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, lastTargetDirection);
    }

    private void FreeAllNativeData()
    {
        steeringNativeData.bestSolution.Dispose(); 
        steeringNativeData.controlValues.Dispose();

        FreeEphemeralNativeData();
    }

    private void FreeEphemeralNativeData()
    {
        steeringNativeData.castCommands.Dispose();
        steeringNativeData.castResults.Dispose();
        steeringNativeData.nativeSteeringOptions.Dispose();
        steeringNativeData.scores.Dispose();
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

    private struct ScoreAllSolutionsJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<RaycastHit> hitResults;
        [ReadOnly]
        public NativeArray<Vector3> steeringOptions;
        public NativeArray<float> scores;
        public Vector3 targetPosition;
        public Vector3 idealDirection;
        public Vector3 currentVelocity;
        public Vector3 currentForward;
        public float lookAheadTime;
        public float castRadius;

        public float onTargetValue;
        public float safeDistanceValue;
        public float targetProxValue;
        public float minimalSteerValue;

        private float TestPotentialPath(RaycastHit hit, float noHitFoundDistance)
        {
            // Hack because apparently can't check for collider's existence on not-main-thread
            bool didHit = hit.point != default;
            return didHit ? hit.distance : noHitFoundDistance; // Can't guarantee an option is good beyond as far as we looked
        }
        public void Execute(int index)
        {
            Vector3 testVel = steeringOptions[index];
            float collisionDistance = TestPotentialPath(hitResults[index], (testVel.magnitude * lookAheadTime) + castRadius);
            float closenessToDesiredDirection = Vector3.Dot(testVel, idealDirection);
            float steeringEffort = Vector3.Dot(currentVelocity, testVel) + Vector3.Dot(currentForward, testVel);
            float targetProx = -Vector3.Distance(targetPosition + lookAheadTime * testVel, targetPosition) / 1000;

            float score = (closenessToDesiredDirection * onTargetValue)
            + (collisionDistance * safeDistanceValue)
            + (targetProx * targetProxValue)
            + (steeringEffort * minimalSteerValue);

            scores[index] = score;
        }
    }

    private struct TakeBestSolutionJob : IJob
    {
        [ReadOnly]
        public NativeArray<Vector3> steeringOptions;
        [ReadOnly]
        public NativeArray<float> scores;
        public NativeArray<Vector3> result;

        public void Execute()
        {
            float maxScore = float.MinValue;
            Vector3 bestDirection = Vector3.forward;

            for(int i = 0; i < scores.Length; ++i)
            {
                if (scores[i] > maxScore)
                {
                    maxScore = scores[i];
                    bestDirection = steeringOptions[i];
                }
            }

            result[0] = bestDirection;
        }
    }

    private JobHandle LookAheadCheck(Vector3 desiredForward, ref SteeringNativeData nativeData)
    {
        //if (TestPotentialPath(desiredForward) > shipControls.ForwardSpeed * lookAheadTime) return desiredForward;

        Vector3[] steeringOptions =
        {
            desiredForward * shipControls.ForwardSpeed,
            -transform.forward * shipControls.ForwardSpeed,
            shipControls.Velocity,
            shipControls.Velocity + (transform.up * 0.5f * shipControls.ForwardSpeed),
            shipControls.Velocity + (-transform.up * 0.5f * shipControls.ForwardSpeed),
            shipControls.Velocity + (transform.right * 0.5f * shipControls.ForwardSpeed),
            shipControls.Velocity + (-transform.right * 0.5f * shipControls.ForwardSpeed)
        };

        nativeData.castCommands = new NativeArray<SpherecastCommand>(steeringOptions.Length, Allocator.TempJob);
        nativeData.castResults = new NativeArray<RaycastHit>(steeringOptions.Length, Allocator.TempJob);
        nativeData.nativeSteeringOptions = new NativeArray<Vector3>(steeringOptions, Allocator.TempJob);
        nativeData.scores = new NativeArray<float>(steeringOptions.Length, Allocator.TempJob);

        for(int i = 0; i < steeringOptions.Length; ++i)
        {
            nativeData.castCommands[i] = new SpherecastCommand(transform.position, castRadius, steeringOptions[i].normalized, steeringOptions[i].magnitude * lookAheadTime, ~(1 << 9));
        }

        JobHandle castJobHandle = SpherecastCommand.ScheduleBatch(nativeData.castCommands, nativeData.castResults, 1, default);
        
        ScoreAllSolutionsJob scoreJobData = new ScoreAllSolutionsJob();
        scoreJobData.hitResults = nativeData.castResults;
        scoreJobData.steeringOptions = nativeData.nativeSteeringOptions;
        scoreJobData.scores = nativeData.scores;
        scoreJobData.targetPosition = targetPosition;
        scoreJobData.idealDirection = desiredForward;
        scoreJobData.currentVelocity = shipControls.Velocity;
        scoreJobData.currentForward = transform.forward;
        scoreJobData.lookAheadTime = lookAheadTime;
        scoreJobData.castRadius = castRadius;
        scoreJobData.onTargetValue = onTargetValue;
        scoreJobData.safeDistanceValue = safeDistanceValue;
        scoreJobData.targetProxValue = targetProxValue;
        scoreJobData.minimalSteerValue = minimalSteerValue;

        // Do score job after cast job
        JobHandle scoreJobHandle = scoreJobData.Schedule(steeringOptions.Length, 1, castJobHandle);

        TakeBestSolutionJob takeBestJob = new TakeBestSolutionJob();
        takeBestJob.scores = nativeData.scores;
        takeBestJob.steeringOptions = nativeData.nativeSteeringOptions;
        takeBestJob.result = nativeData.bestSolution;

        // Do take best job after score job
        JobHandle takeBestHandle = takeBestJob.Schedule(scoreJobHandle);

        return takeBestHandle;
    }

    private struct GetSteerInputJob : IJob
    {
        [ReadOnly]
        public NativeArray<Vector3> desiredForwardNative;

        public Vector3 currentForward;
        public Vector3 currentUp;
        public Vector3 currentRight;
        public float largeDeflectionThreshold;
        public float magicThresholdValue;
         
        public float rollAttack, rollSlowDown, acceptableRollWindow;
        public float pitchAttack, pitchSlowDown;
        public float yawAttack, yawSlowDown;

        public NativeArray<float> controlValues; // pitch, yaw, roll, throttle

        public void Execute()
        {
            Vector3 desiredForward = desiredForwardNative[0];
            float angleFromTarget = Vector3.Angle(currentForward, desiredForward);

            bool isSignificantDeflection = angleFromTarget > largeDeflectionThreshold;

            float pitch = 0;
            float yaw = 0;
            float roll = 0;
            float throttle = 0;

            if (isSignificantDeflection)
            {
                // For significant deflections the ship should roll to allow the maneuver to be completed with pitch
                Vector3 desiredRollUp = Vector3.ProjectOnPlane(desiredForward, currentForward);
                float neededRollAngle = 0;
                if (desiredRollUp.sqrMagnitude > magicThresholdValue)
                {
                    desiredRollUp.Normalize();
                    neededRollAngle = Vector3.SignedAngle(currentUp, desiredRollUp, currentForward);
                }

                roll = -1 * neededRollAngle / rollAttack;
                throttle -= Mathf.Abs(neededRollAngle / rollSlowDown);

                if (Mathf.Abs(neededRollAngle) < acceptableRollWindow)
                {
                    Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, currentRight);
                    float neededPitchAngle = Vector3.SignedAngle(currentForward, pitchDesForward, currentRight);
                    pitch = neededPitchAngle / pitchAttack;
                    throttle -= Mathf.Abs(neededPitchAngle / pitchSlowDown);
                }
            }
            else
            {
                Vector3 yawDesForward = Vector3.ProjectOnPlane(desiredForward, currentUp);
                float neededYawAngle = yawDesForward.sqrMagnitude > magicThresholdValue ? -Vector3.SignedAngle(yawDesForward, currentForward, currentUp) : 0;
                yaw = neededYawAngle / yawAttack;
                throttle -= Mathf.Abs(neededYawAngle / yawSlowDown);

                Vector3 pitchDesForward = Vector3.ProjectOnPlane(desiredForward, currentRight);
                float neededPitchAngle = pitchDesForward.sqrMagnitude > magicThresholdValue ? -Vector3.SignedAngle(pitchDesForward, currentForward, currentRight) : 0;
                pitch = neededPitchAngle / pitchAttack;
                throttle -= Mathf.Abs(neededPitchAngle / pitchSlowDown);
                throttle += 1;
            }

            controlValues[0] = pitch;
            controlValues[1] = yaw;
            controlValues[2] = roll;
            controlValues[3] = throttle;
        }
    }
}
