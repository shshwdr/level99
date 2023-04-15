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

    public override float costPerSecond => 3;

    protected override List<Character> canSelectCharacters()
    {
        List<Character> res = new List<Character>();
        foreach (var patient in PatientManager.Instance.patients)
        {
            if (!patient.hasType(typeof(HealLinkSkill)))
            {
                
                res.Add(patient);
            }
        }
        return res;
    }

    public override bool init()
    {
        if (canSelectCharacters().Count == 0)
        {
            FloatingTextManager.Instance.addText("No Available Targe", Vector3.zero, Color.white);
            return false;
        }
        var go = Instantiate(Resources.Load<GameObject>("healingLine"),transform);
        line = go.GetComponentInChildren<DynamicLine>();
        line.gameObject.SetActive(false);
        base.init();
        return true;
    }

    public override void unconnect()
    {
        
        
        if (currentPatient)
        {
            
            currentPatient.unconnectSkill(this);
        }
        Destroy(line.gameObject);
        base.unconnect();
    }

    public void UseSkill(Patient patient)
    {
        currentPatient = patient;
    }

    public override bool click(Character character)
    {
        base.click(character);
        if (character is Patient patient && !patient. hasType(typeof(HealLinkSkill)))
        {
            
            
            UseSkill(patient);
            line.gameObject.SetActive(true);
            line.startPoint = transform;
            line.endPoint = character.transform;
            patient.connectSkill(this);
            return true;
        }

        return false;
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
