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
    BoundaryTriggerType secondTrigger;
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
                secondTrigger = trigger.type;
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
                    setLandWaterState(secondTrigger);
                    Debug.Log("Switched land and sea!");
                }

                secondTrigger = BoundaryTriggerType.NONE;
            }
        }
    }

    private void setLandWaterState(BoundaryTriggerType state)
    {
        waterLocomotionController.enabled = state == BoundaryTriggerType.WATER;
        groundPlayerController.enabled = state == BoundaryTriggerType.AIR;
        
        if (state == BoundaryTriggerType.WATER)
        {
            rb.gravityScale = waterGravityScale;
            GetComponent<BreathControl>().StartBreathHold();
            AudioManager.Instance.SetUrgency(25);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.submerge, gameObject.transform.position);
        }
        else
        {
            rb.gravityScale = landGravityScale;
            GetComponent<BreathControl>().StopBreathHold();
            AudioManager.Instance.SetUrgency(0);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.emerge, gameObject.transform.position);

        }
    }
}
