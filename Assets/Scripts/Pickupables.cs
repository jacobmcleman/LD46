using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupables : MonoBehaviour
{
    public enum PickupType
    {
        Organic,
        Mechanical,
        Health
    }

    private PickupType pickType;
    private int value = 10;

    public int getValue() { return value; }
    public PickupType getType() { return pickType; }

    public void Initialize(int _value)
    {
        value = _value;
    } 
}
