using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Grapple : MonoBehaviour
{
    [SerializeField] Transform grappleOrigin;
    private Vector3 grapplePoint;
    bool isGrappled = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float grappleForce = 4f;
    [SerializeField] float grappleTurn = 10f;

    [FormerlySerializedAs("joint")] [FormerlySerializedAs("distanceJoint")] [SerializeField] private DistanceJoint2D grappleJoint;

    private WaterLocomotionController waterController;
    // Start is called before the first frame update
    void Start()
    {
        waterController = GetComponent<WaterLocomotionController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
    }

    private void HandleInput()
    {
        if (waterController.isMovementRestricted) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 grappleDirection = mouseWorldPoint - grappleOrigin.position;
            RaycastHit2D hit = Physics2D.Raycast(grappleOrigin.position, grappleDirection);
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Grappleable>() != null)
                {
                    grapplePoint = hit.point;
                    isGrappled = true;
                    grappleJoint.enabled = true;
                    grappleJoint.transform.position = hit.point;
                    //distanceJoint.distance = (hit.point - new Vector2(grappleOrigin.position.x, grappleOrigin.position.y)).magnitude;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isGrappled)
            {
                lineRenderer.SetPositions(new[] { grappleOrigin.position, grapplePoint });
                lineRenderer.positionCount = 2;
                
                Debug.Log($"Reaction Force: {grappleJoint.reactionForce.magnitude}");
                if (grappleJoint.reactionForce.magnitude <= 0)
                {
                    rigidbody2D.AddForce(grappleForce * Time.deltaTime *(grapplePoint - grappleOrigin.position).normalized, ForceMode2D.Impulse);
                }
                /*transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(Vector3.forward, grapplePoint),
                                                  grappleTurn * Time.deltaTime);*/
            }
        }
        else
        {
            isGrappled = false;
            lineRenderer.positionCount = 0;
            grappleJoint.enabled = false;

        }
    }
}
