using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class BreathControl : MonoBehaviour
{
    private float currentBreathSeconds = 20;
    public bool isHoldingBreath = false;
    private float breathDecayRate = 1f;
    public float maxBreathSeconds = 20;
    private float firstWarningTimeDuration = 6f;
    private float lastWarningTimeDuration = 6f;

    // Start is called before the first frame update
    void Start()
    {
        ResetBreath();
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
        AudioManager.Instance.SetUrgency(100);

        StartCoroutine(ResetLevel());
        GetComponent<WaterLocomotionController>().setMovementRestriction(true);
        //restrict player input
        //death animation
        //vignette camera
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(3);


        ResetBreath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GetComponent<WaterLocomotionController>().setMovementRestriction(false);
    }

    private void ResetBreath()
    {
        currentBreathSeconds = maxBreathSeconds;
        isFirstWarningGiven = false;
        isLastWarningGiven = false;
        GetComponent<MMF_Player>().StopFeedbacks();
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
        //Debug.Log($"Breath: {currentBreathSeconds}");
        if (!isFirstWarningGiven && currentBreathSeconds < (lastWarningTimeDuration + firstWarningTimeDuration))
        {
            isFirstWarningGiven = true;
            Debug.Log("I should hurry");
            AudioManager.Instance.SetUrgency(50);

        }
        else if(!isLastWarningGiven && currentBreathSeconds < lastWarningTimeDuration)
        {
            isLastWarningGiven = true;
            Debug.Log("Prolly gonna die soon.");
            AudioManager.Instance.SetUrgency(75);
            GetComponent<MMF_Player>().PlayFeedbacks();
            
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
