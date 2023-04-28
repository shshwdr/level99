using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class BreathControl : MonoBehaviour
{
    private float _currentBreathSeconds = 20;
    public bool isHoldingBreath = false;
    private float _breathDecayRate = 1f;
    public float maxBreathSeconds = 20;
    private float _firstWarningTimeDuration = 6f;
    private float _lastWarningTimeDuration = 6f;

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


        if (_currentBreathSeconds < 0)
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
        _currentBreathSeconds = maxBreathSeconds;
        _isFirstWarningGiven = false;
        _isLastWarningGiven = false;
        GetComponent<MMF_Player>().StopFeedbacks();
    }

    private void UpdateUI()
    {
        throw new NotImplementedException();
    }

    bool _isFirstWarningGiven = false;
    bool _isLastWarningGiven = false;
    private void HandleHoldBreathUpdate()
    {
        _currentBreathSeconds -= _breathDecayRate * Time.deltaTime;
        //Debug.Log($"Breath: {currentBreathSeconds}");
        if (!_isFirstWarningGiven && _currentBreathSeconds < (_lastWarningTimeDuration + _firstWarningTimeDuration))
        {
            _isFirstWarningGiven = true;
            Debug.Log("I should hurry");
            AudioManager.Instance.SetUrgency(50);

        }
        else if(!_isLastWarningGiven && _currentBreathSeconds < _lastWarningTimeDuration)
        {
            _isLastWarningGiven = true;
            Debug.Log("Prolly gonna die soon.");
            AudioManager.Instance.SetUrgency(75);
            GetComponent<MMF_Player>().PlayFeedbacks();
            
        }
    }

    internal void StartBreathHold()
    {
        isHoldingBreath = true;
    }

    internal void StopBreathHold()
    {
        isHoldingBreath = false;
        ResetBreath();
    }
}
