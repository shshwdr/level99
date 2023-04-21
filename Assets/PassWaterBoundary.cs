using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BoundaryTrigger;

public class PassWaterBoundary : MonoBehaviour
{
    WaterLocomotionController waterLocomotionController;
    GroundPlayerController groundPlayerController;
    Rigidbody2D rb;
    [SerializeField]  private float waterGravityScale = .2f;
    [SerializeField] private float landGravityScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        waterLocomotionController = GetComponent<WaterLocomotionController>();
        groundPlayerController = GetComponent<GroundPlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    BoundaryTriggerType firstTrigger;
    bool firstTriggerEntered = false;
    bool secondTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoundaryTrigger trigger = collision.GetComponent<BoundaryTrigger>();
        if (trigger != null)
        {
            if (firstTrigger == BoundaryTriggerType.NONE)
            {
                firstTrigger = trigger.type;
                firstTriggerEntered = true;
            }
            else
            {
                secondTriggerEntered = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BoundaryTrigger trigger = collision.GetComponent<BoundaryTrigger>();
        if (trigger != null)
        {
            if (firstTrigger == trigger.type)
            {
                firstTriggerEntered = false;
            }
            else 
            {
                secondTriggerEntered = false;

                if (firstTriggerEntered == false)
                {
                    //we have crossed successfully
                    firstTrigger = BoundaryTriggerType.NONE;
                    toggleLandWaterState();
                    Debug.Log("Switched land and sea!");
                }
            }
        }
    }

    private void toggleLandWaterState()
    {
        waterLocomotionController.enabled = !waterLocomotionController.enabled;
        groundPlayerController.enabled = !groundPlayerController.enabled;
        
        if (waterLocomotionController.enabled == true)
        {
            rb.gravityScale = waterGravityScale;
        }
        else
        {
            rb.gravityScale = landGravityScale;
        }
    }
}
