using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHud : MonoBehaviour
{
    public Transform Target;
    public float uiElementSize = 50.0f;

    public GameObject IndicatorUI;

    private GameObject OnScreenIndicator;
    private GameObject OffScreenIndicator;

    private void Start()
    {
        OnScreenIndicator = IndicatorUI.transform.Find("OnScreenIndicator").gameObject;
        OffScreenIndicator = IndicatorUI.transform.Find("OffScreenDirection").gameObject;
    }

    private void Update()
    {
        if (Target != null)
        {
            Vector2 hudPos;

            if (TargetVisible)
            {
                OnScreenIndicator.SetActive(true);
                OffScreenIndicator.SetActive(false);

                hudPos = ScreenSpaceObjPos;
            }
            else
            {
                OnScreenIndicator.SetActive(false);
                OffScreenIndicator.SetActive(true);

                hudPos = ScreenEdgeObjPos;
                float angle = Vector2.Angle(CamBounds.center - hudPos, Vector2.up);
                OffScreenIndicator.transform.eulerAngles = new Vector3(0, 0, angle);
            }

            Vector3 hudPos3 = new Vector3(hudPos.x, hudPos.y, IndicatorUI.transform.position.z);
            IndicatorUI.transform.position = hudPos3;
        }
        else
        {
            OnScreenIndicator.SetActive(false);
            OffScreenIndicator.SetActive(false);
        }
    }

    private Rect CamBounds
    {
        get { Camera cam = Camera.main; return new Rect(uiElementSize, uiElementSize, cam.pixelWidth - (uiElementSize * 2), cam.pixelHeight - (uiElementSize * 2)); }
    }

    private bool TargetVisible
    {
        get => IsPointVisible(Target.position);
    }

    private Vector2 ScreenSpaceObjPos
    {
        get {
            Camera cam = Camera.main;
            Vector3 screenPos3 = cam.WorldToScreenPoint(Target.position);
            Vector2 screenPos = new Vector2(screenPos3.x, screenPos3.y);
            return moveToScreen(screenPos);
        }
    }

    private Vector2 ScreenEdgeObjPos
    {
        get
        {
            Camera cam = Camera.main;
            Vector3 screenPos3 = cam.WorldToScreenPoint(Target.position);
            Vector2 screenPos = new Vector2(screenPos3.x, screenPos3.y);
            return moveToScreenEdge(screenPos);
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

        //return testPoint.y > (((linePointB.y - linePointA.y) / (linePointB.x - linePointA.x)) * (testPoint.x - linePointA.x) + linePointA.y);
    }

    private Vector2 moveToScreenEdge(Vector2 point)
    {
        Rect camBounds = CamBounds;

        bool aboveA = AboveLine(point, camBounds.min, camBounds.max);
        bool aboveB = AboveLine(point, new Vector2(camBounds.min.x, camBounds.max.y), new Vector2(camBounds.max.x, camBounds.min.y));

        if (aboveA == aboveB)
        {
            return new Vector2(
                Mathf.Clamp(point.x, camBounds.xMin, camBounds.xMax),
                point.y < camBounds.center.y ? camBounds.yMin : camBounds.yMax
            );
        }
        else
        {
            return new Vector2(
                point.x < camBounds.center.x ? camBounds.xMin : camBounds.xMax,
                Mathf.Clamp(point.y, camBounds.yMin, camBounds.yMax)
            );
        }
    }
}
