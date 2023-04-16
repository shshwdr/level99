
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill:MonoBehaviour
{
    public virtual float costPerSecond { get; }
    public virtual float range { get; }
    public abstract void hoverOver(Character character);

    public virtual bool click(Character character)
    {
        clearSelectPreview();
        PlayerSkillManager.Instance.rangeObject.localScale = Vector3.zero;
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

    protected abstract List<Character> canSelectCharacters();
    protected abstract List<Character> canSelectCharactersRoughly();


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

