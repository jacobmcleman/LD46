using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHud : MonoBehaviour
{
    private Transform target;
    private SpaceshipController targetShip;
    private SpaceshipController myShip;

    public string InitialTargetTag = "";

    public Transform Target
    {
        get { return target; }
        set { if (value != target) { target = value; targetShip = target.GetComponent<SpaceshipController>(); } }
    }


    public float uiElementSize = 100.0f;

    public GameObject IndicatorUI;

    public bool LeadPip = false;

    public Projectile projPrefab;

    private GameObject OnScreenIndicator;
    private GameObject OffScreenIndicator;
    private GameObject TargetLeadIndicator;
    private GameObject TargetDistanceText;

    private void Start()
    {
        if(InitialTargetTag != "")
        {
            GameObject initialTargetObj = GameObject.FindGameObjectWithTag(InitialTargetTag);
            Target = initialTargetObj != null ? initialTargetObj.transform : null;
        }

        OnScreenIndicator = IndicatorUI.transform.Find("OnScreenIndicator").gameObject;
        OffScreenIndicator = IndicatorUI.transform.Find("OffScreenDirection").gameObject;
        TargetLeadIndicator = IndicatorUI.transform.Find("LeadPip").gameObject;
        TargetDistanceText = IndicatorUI.transform.Find("DistanceText").gameObject;

        myShip = GetComponent<SpaceshipController>();
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            Vector2 hudPos;

            if (TargetVisible)
            {
                OnScreenIndicator.SetActive(true);
                OffScreenIndicator.SetActive(false);
                TargetDistanceText.SetActive(true);

                hudPos = ScreenSpaceObjPos;

                if (CanLead)
                {
                    TargetLeadIndicator.SetActive(true);
                    DoLeadPip();
                }
                else
                {
                    TargetLeadIndicator.SetActive(false);
                }
            }
            else
            {
                OnScreenIndicator.SetActive(false);
                OffScreenIndicator.SetActive(true);
                TargetLeadIndicator.SetActive(false);
                TargetDistanceText.SetActive(true);

                hudPos = ScreenSpaceObjPos;
                float angle = -Vector2.SignedAngle(CamBounds.center - hudPos, Vector2.down);
                OffScreenIndicator.transform.eulerAngles = new Vector3(0, 0, angle);
            }

            Vector3 hudPos3 = new Vector3(hudPos.x, hudPos.y, IndicatorUI.transform.position.z);
            IndicatorUI.transform.position = hudPos3;

            float distance = Vector3.Distance(transform.position, Target.position);
            TargetDistanceText.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0:F1}m", distance);
        }
        else
        {
            OnScreenIndicator.SetActive(false);
            OffScreenIndicator.SetActive(false);
            TargetLeadIndicator.SetActive(false);
            TargetDistanceText.SetActive(false);
        }
    }

    private bool CanLead
    {
        get { return LeadPip && myShip != null && targetShip != null; }
    }

    private void DoLeadPip()
    {
        Vector3 relativeVelocity = myShip.Velocity + targetShip.Velocity;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        float projectileSpeed = projPrefab.speed + myShip.Velocity.magnitude;
        float timeToTarget = distanceToTarget / projectileSpeed;
        Vector3 aheadVector = timeToTarget * relativeVelocity;
        Vector3 pipWorldPos = target.position + aheadVector;
        Vector2 pipScreenPos = WorldPointToScreen(pipWorldPos);
        Vector3 hudPos3 = new Vector3(pipScreenPos.x, pipScreenPos.y, TargetLeadIndicator.transform.position.z);
        TargetLeadIndicator.transform.position = hudPos3;
    }

    private Rect CamBounds
    {
        get { Camera cam = Camera.main; return new Rect(uiElementSize, uiElementSize, cam.pixelWidth - (uiElementSize * 2), cam.pixelHeight - (uiElementSize * 2)); }
    }

    private bool TargetVisible
    {
        get => IsPointVisible(Target.position);
    }

    private float BehindMultiplier
    {
        get
        {
            Camera cam = Camera.main;
            Vector3 toTarget = Target.position - cam.transform.position;
            return Mathf.Sign(Vector3.Dot(toTarget, cam.transform.forward));
        }
    }

    private Vector2 WorldPointToScreen(Vector3 worldPos)
    {
        Camera cam = Camera.main;
        Vector3 screenPos3 = BehindMultiplier * cam.WorldToScreenPoint(worldPos);
        Vector2 screenPos = new Vector2(screenPos3.x, screenPos3.y);
        return moveToScreen(screenPos);
    }

    private Vector2 ScreenSpaceObjPos
    {
        get {
            return WorldPointToScreen(target.position);
        }
    }

    private bool IsPointVisible(Vector3 position)
    {
        Camera cam = Camera.main;
        Vector3 toTarget = position - cam.transform.position;

        // Early out if point is behind camera
        if (Vector3.Dot(toTarget, cam.transform.forward) < 0) return false;

        Vector3 screenPos3 = cam.WorldToScreenPoint(position);
        Vector2 screenPos = new Vector2(screenPos3.x, screenPos3.y);
        return CamBounds.Contains(screenPos);
    }

    private Vector2 moveToScreen(Vector2 point)
    {
        Rect camBounds = CamBounds;
        return new Vector2(
            Mathf.Clamp(point.x, camBounds.xMin, camBounds.xMax),
            Mathf.Clamp(point.y, camBounds.yMin, camBounds.yMax)
        );
    }

    private bool AboveLine(Vector2 testPoint, Vector2 linePointA, Vector2 linePointB)
    {

        return testPoint.y > (((linePointB.y - linePointA.y) / (linePointB.x - linePointA.x)) * (testPoint.x - linePointA.x) + linePointA.y);
    }
}
