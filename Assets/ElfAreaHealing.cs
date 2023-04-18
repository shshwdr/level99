using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ElfAreaHealing : MonoBehaviour
{
    public float healDistance;

    public float healAmount = 3;
    float healingTimer = 0;

    public float healTime = 1;

    public float surviveTime = 5;

    private float surviveTimer = 0;

    private bool startShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.DOKill();
    }

    // Update is called once per frame
    void Update()
    {
        surviveTimer += Time.deltaTime;
        healingTimer += Time.deltaTime;
        if (healingTimer > healTime)
        {
            healingTimer = 0;
            for(int i = 0;i<PatientManager.Instance.patients.Count;i++)
            {
                var patient = PatientManager.Instance.patients[i];
                var distance = Vector3.Distance(patient.transform.position,
                    transform.position);
                if (distance < healDistance/2f)
                {
                    patient.Heal(healAmount);
                    if (!patient || !PatientManager.Instance.patients.Contains(patient))
                    {
                        i--;
                    }
                }
            }
        }

        float shakingTime = 0.2f;
        if (surviveTimer >= surviveTime * (1 - shakingTime) && !startShaking)
        {
            startShaking = true;
            transform.DOShakePosition(surviveTime * shakingTime * 2, 1, 50);
        }

        if (surviveTimer >= surviveTime)
        {
            gameObject.SetActive(false);
        }
    }
}