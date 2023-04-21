using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    public enum BoundaryTriggerType { 
        NONE = 0,
        WATER = 1,
        AIR = 2}
    public BoundaryTriggerType type;
}
