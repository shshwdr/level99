using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DynamicLine : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    public LineRenderer lineRenderer;
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.1f;
    public int numPoints = 10;
    private float shakeScale = 0;
    private float shakeTimer = 0f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (shakeScale > 0f)
        {
            lineRenderer.positionCount = numPoints;
            // Generate a random offset for each point in the line renderer
            for (int i = 0; i < numPoints; i++)
            {
                Vector3 startPos = startTransform.position + Random.insideUnitSphere * shakeMagnitude * shakeScale;
                Vector3 endPos = endTransform.position + Random.insideUnitSphere * shakeMagnitude * shakeScale;

                float t = (float)i / (numPoints - 1);
                Vector3 pointPos = Vector3.Lerp(startPos, endPos, t);

                lineRenderer.SetPosition(i, pointPos);
            }

        }
        else
        {
            lineRenderer.positionCount = 2;
            // Set the positions of the line renderer points to the actual positions of the transforms
            lineRenderer.SetPosition(0, startTransform.position);
            lineRenderer.SetPosition(1, endTransform.position);
        }
    }

    public void Shake(float scale)
    {
        shakeScale = scale;
    }
}
