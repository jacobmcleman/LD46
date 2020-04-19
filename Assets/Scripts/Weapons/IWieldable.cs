using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWieldable
{
    /** team of object */
    Teams team { get; set; }
    GameObject gameObject { get; }
    Transform transform { get; }
}
