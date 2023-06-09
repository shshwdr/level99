
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill:MonoBehaviour
{
    public virtual float costPerSecond { get; }
    public virtual float range { get; }
    public bool isSelecting;
    

    public virtual void hoverOver(Character character)
    {
        
        if (canSelectCharacters().Contains(character))
        {
            character.hoverOver();
        }
    }

    public virtual bool mouseDown(Character character)
    {
        clearSelectPreview();
        isSelecting = false;
        return true;
    }

    void clearSelectPreview()
    {
        
        foreach (var patient in canSelectCharacters())
        {
            patient.stopCanSelect();
        }
    }
    public virtual void unconnect()
    {
        clearSelectPreview();
        Destroy(this);
    }

    protected virtual bool skillSpecialCondition(Patient patient)
    {
        return true;
    }
    protected virtual List<Character> canSelectCharacters()
    {
        List<Character> res = new List<Character>();
        foreach (var patient in PatientManager.Instance.patients)
        {
            if (patient.isActive &&  skillSpecialCondition(patient)&& Vector3.Distance( patient.transform.position,PlayerSkillManager.Instance.transform.position)<range)
            {
                
                res.Add(patient);
            }
        }
        return res;
    }
    protected virtual List<Character> canSelectCharactersRoughly()
    {
        List<Character> res = new List<Character>();
        foreach (var patient in PatientManager.Instance.patients)
        {
            if (patient.isActive &&  !patient.hasType(typeof(HealLinkSkill)))
            {
                
                res.Add(patient);
            }
        }
        return res;
    }

    public virtual bool init()
    {
        
        foreach (var patient in canSelectCharacters())
        {
            patient.showCanSelect();
        }
        PlayerSkillManager.Instance.rangeObject.localScale = Vector3.one*range*2;
        return true;
    }

    protected virtual void Update()
    {
        if (isSelecting)
        {
            
            foreach (var patient in PatientManager.Instance.patients)
            {
                patient.stopCanSelect();
            }
            foreach (var patient in canSelectCharacters())
            {
                patient.showCanSelect();
            }
        }
    }
}

