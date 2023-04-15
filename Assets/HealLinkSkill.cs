using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealLinkSkill:Skill
{
    private Patient currentPatient;
    public float HealTime = 0.5f;
    private float healTimer = 0;
    public float healAmount = 5;
    private DynamicLine line;
    

    private void Start()
    {
        var go = Instantiate(Resources.Load<GameObject>("healingLine"),transform);
        line = go.GetComponentInChildren<DynamicLine>();
        line.gameObject.SetActive(false);
    }

    public override void unconnect()
    {
        Destroy(line);
        base.unconnect();
    }

    public void UseSkill(Patient patient)
    {
        currentPatient = patient;
    }

    public override void click(Character character)
    {
        
        if (character is Patient patient)
        {
            UseSkill(patient);
            line.gameObject.SetActive(true);
            line.startPoint = transform;
            line.endPoint = character.transform;
        }
    }
    public override void hoverOver(Character character)
    {
        if (character is Patient)
        {
            character.hoverOver();
        }
    }

    void Update()
    {
        if (currentPatient)
        {
            
            healTimer += Time.deltaTime;
            if (healTimer > HealTime)
            {
                currentPatient.Heal(healAmount);
                healTimer = 0;
            }
        }
    }
}
