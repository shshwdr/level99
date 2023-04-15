using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Patient : Character
{
    private float _hp = 100;

    public float hpDecreasePerSecond = 5;

    private HPBar _hpBar;

    public List<Skill> connectedSkills = new List<Skill>();

    private void Awake()
    {
        _hpBar = GetComponentInChildren<HPBar>();
    }

    public bool hasType(Type skillType)
    {
        bool hasB = connectedSkills.Where(item => item.GetType() == skillType).Any();
        return hasB;
    }
    public void connectSkill(Skill skill)
    {
        connectedSkills.Add(skill);
    }
    public void unconnectSkill(Skill skill)
    {
        connectedSkills.Remove(skill);
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


    public void Heal(float hp)
    {
        _hp += hp;
        
        _hpBar.UpdateHP((int)_hp);
    }
}
