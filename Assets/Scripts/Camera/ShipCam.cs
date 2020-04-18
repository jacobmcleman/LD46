﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCam : MonoBehaviour
{
    public SpaceshipController followShip;

    public float lookDistance = 10;
    public float verticalOffset = 3;

    public float velocityWeight = 1.0f;
    public float velocitySecondsAhead = 0.5f;

    public float forwardWeight = 1.0f;
    public float forwardDistanceAhead = 0.5f;
    
    public float shipPositionWeight = 1.0f;

    private Rigidbody followRb;
    private Transform followTransform;

    private void Start()
    {
        followRb = followShip.GetComponent<Rigidbody>();
        followTransform = followShip.GetComponent<Transform>();
    }

    void Update()
    {
        float totalWeight = velocityWeight + forwardWeight + shipPositionWeight;

        Vector3 velPoint = followTransform.position + (followRb.velocity * velocitySecondsAhead);
        Vector3 forwardPoint = followTransform.position + (followTransform.forward * forwardDistanceAhead);

        Vector3 lookPoint = (1 / totalWeight) * ((velocityWeight * velPoint) + (forwardWeight * forwardPoint) + (shipPositionWeight * followTransform.position));

        Vector3 lookDir = (lookPoint - followTransform.position).normalized;

        transform.position = followTransform.position - (lookDistance * lookDir) + (followTransform.up * verticalOffset);
        transform.LookAt(lookPoint, followTransform.up);
    }
}