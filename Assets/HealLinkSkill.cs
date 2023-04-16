using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class HealLinkSkill:Skill
{
    private Patient currentPatient;
    public float HealTime = 0.5f;
    private float healTimer = 0;
    public float healAmount = 4;
    private DynamicLine line;
    

    public override float costPerSecond => 3;
    public override float range => 20;

    
    
    

    public override bool init()
    {
        if (canSelectCharactersRoughly().Count == 0)
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
        if (canSelectCharacters().Contains(character) )
        {
            var patient = character as Patient;
            
            UseSkill(patient);
            line.gameObject.SetActive(true);
            line.startTransform = transform;
            line.endTransform = character.transform;
            patient.connectSkill(this);
            return true;
        }

        return false;
    }

     protected override void Update()
    {
        base.Update();
        if (currentPatient)
        {
            
            healTimer += Time.deltaTime;
            if (healTimer > HealTime)
            {
                currentPatient.Heal(healAmount);
                healTimer = 0;
            }

            var distance = Vector3.Distance(currentPatient.transform.position,
                PlayerSkillManager.Instance.transform.position);
            float startShakingRange = 0.5f;
            if ( distance> range * startShakingRange)
            {
                line.Shake( 1 - (range - distance )/(range*(1-startShakingRange))) ;
            }

            if (distance > range)
            {
                PlayerSkillManager.Instance.unconnectSkill(this);
            }
        }
    }
}
