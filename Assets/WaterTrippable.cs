using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrippable : MonoBehaviour
{
    [SerializeField] private WaterLocomotionController waterController;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WaterTripper tripper = collision.GetComponent<WaterTripper>();
        if (tripper != null)
        {
            StartCoroutine(HandleTrip(tripper.tripLengthSeconds, tripper.tripForceMagnitude));
        }
    }

    IEnumerator HandleTrip(float tripLengthSeconds, float tripForceMagnitude)
    {
        waterController.setMovementRestriction(true);
        waterController.AddStoppingForce(tripForceMagnitude);
        animator.Play("water_trip");

        yield return new WaitForSeconds(tripLengthSeconds);

        waterController.setMovementRestriction(false);
        animator.Play("swimming");

    }
}
