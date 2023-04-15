using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] Transform grappleOrigin;
    private Vector3 grapplePoint;
    bool isGrappled = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float grappleForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                }
            }
        } else if (Input.GetMouseButton(0))
        {
            if (isGrappled)
            {
                lineRenderer.SetPositions(new[] { grappleOrigin.position, grapplePoint });
                lineRenderer.positionCount = 2;
                rigidbody2D.AddForce(grapplePoint.normalized * grappleForce, ForceMode2D.Force);
            }
        }
        else
        {
            isGrappled = false;
        }
        
    }
}
