using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : Character
{
    private float _hp = 100;

    public float hpDecreasePerSecond = 5;

    private HPBar _hpBar;

    private void Awake()
    {
        _hpBar = GetComponentInChildren<HPBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hp -= hpDecreasePerSecond * Time.deltaTime;
        _hpBar.UpdateHP((int)_hp);
    }

    public void canSelect()
    {
        
    }

    public void Heal(float hp)
    {
        _hp += hp;
        
        _hpBar.UpdateHP((int)_hp);
    }
}
