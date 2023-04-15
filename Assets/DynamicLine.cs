using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLine : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
    }

    void Update()
    {
        _lineRenderer.SetPosition(0, startPoint.position);
        _lineRenderer.SetPosition(1, endPoint.position);
    }
}
