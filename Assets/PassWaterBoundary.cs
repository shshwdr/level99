using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassWaterBoundary : MonoBehaviour
{
    [SerializeField] WaterLocomotionController waterLocomotionController;
    [SerializeField] GroundPlayerController groundPlayerController;
    [SerializeField] Rigidbody2D rb;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WaterBoundaryTrigger>() != null)
        {
            toggleLandWaterState();
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
