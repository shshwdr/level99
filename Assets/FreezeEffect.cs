using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    private float freezeTime = 5;

    private float timer = 0;

    private bool startShaking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init()
    {
        gameObject.SetActive(true);
        timer = 0;
        transform.localPosition = Vector3.zero;
        startShaking = false;
        transform.DOKill();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float shakingTime = 0.2f;
        if (timer >= freezeTime * (1-shakingTime) && !startShaking)
        {
            startShaking = true;
            transform.DOShakePosition(freezeTime * shakingTime*2,1,50);
        }
        if (timer >= freezeTime)
        {
            gameObject.SetActive(false);
            GetComponentInParent<Patient>().unfreeze();
        }
        
    }
}
