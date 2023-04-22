using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathControl : MonoBehaviour
{
    public float currentBreathSeconds = 15;
    public bool isHoldingBreath = false;
    private float breathDecayRate = 1f;
    private float maxBreathSeconds = 15;
    private float firstWarningTimeDuration = 4f;
    private float lastWarningTimeDuration = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingBreath)
        {
            HandleHoldBreathUpdate();
        }


        if (currentBreathSeconds < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Death! :(");


        //restrict player input
        //death animation
        //vignette camera
        ResetBreath();
    }

    private void ResetBreath()
    {
        currentBreathSeconds = maxBreathSeconds;
        isFirstWarningGiven = false;
        isLastWarningGiven = false;
    }

    private void UpdateUI()
    {
        throw new NotImplementedException();
    }

    bool isFirstWarningGiven = false;
    bool isLastWarningGiven = false;
    private void HandleHoldBreathUpdate()
    {
        currentBreathSeconds -= breathDecayRate * Time.deltaTime;
        if (!isFirstWarningGiven && currentBreathSeconds - lastWarningTimeDuration < 0)
        {
            isFirstWarningGiven = true;
            Debug.Log("I should hurry");
        } else if( currentBreathSeconds - lastWarningTimeDuration - firstWarningTimeDuration < 0)
        {
            isLastWarningGiven = true;
            Debug.Log("Prolly gonna die soon.");
        }
    }

    internal void startBreathHold()
    {
        isHoldingBreath = true;
    }

    internal void stopBreathHold()
    {
        isHoldingBreath = false;
        ResetBreath();
    }
}
